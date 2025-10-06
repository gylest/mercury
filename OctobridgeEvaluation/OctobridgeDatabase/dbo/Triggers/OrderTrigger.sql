CREATE TRIGGER tblTriggerAuditRecord on [Order]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
   SET NOCOUNT ON;

   DECLARE @AuditActionType NVARCHAR(2);
   DECLARE @AuditType       NVARCHAR(2);

    SET @AuditActionType = 'I'; -- Set Action to Insert by default.
    IF EXISTS(SELECT * FROM DELETED)
    BEGIN
        SET @AuditActionType = 
            CASE
                WHEN EXISTS(SELECT * FROM INSERTED) THEN 'U' -- Set Action to Updated.
                ELSE 'D' -- Set Action to Deleted.       
            END
    END
    ELSE 
        IF NOT EXISTS(SELECT * FROM INSERTED) RETURN; -- Nothing updated or inserted.

  IF @AuditActionType='I' OR @AuditActionType='U'
  BEGIN
      IF @AuditActionType='I' SET @AuditType='I' ELSE  SET @AuditType='UA';

  insert into OrderAudit 
  (Id, OrderStatus, CustomerId, FreightAmount, SubTotal, TotalDue, PaymentDate,ShippedDate,CancelDate,RecordCreated,RecordModified,AuditType, UpdatedBy, UpdatedOn )
  select i.Id, i.OrderStatus, i.CustomerId, i.FreightAmount, i.SubTotal, i.TotalDue, i.PaymentDate,i.ShippedDate,i.CancelDate,i.RecordCreated,i.RecordModified,@AuditType,SUSER_SNAME(), getdate() 
  from  [Order] o
  inner join inserted i on o.Id=i.Id
  END

  IF @AuditActionType='D' OR @AuditActionType='U'
  BEGIN
        IF @AuditActionType='D' SET @AuditType='D' ELSE  SET @AuditType='UB';

        IF @AuditType='UB'
  insert into OrderAudit 
  (Id, OrderStatus, CustomerId, FreightAmount, SubTotal, TotalDue, PaymentDate,ShippedDate,CancelDate,RecordCreated,RecordModified,AuditType, UpdatedBy, UpdatedOn )
  select d.Id, d.OrderStatus, d.CustomerId, d.FreightAmount, d.SubTotal, d.TotalDue, d.PaymentDate,d.ShippedDate,d.CancelDate,d.RecordCreated,d.RecordModified,@AuditType,SUSER_SNAME(), getdate() 
  from [Order] o
  inner join deleted d on o.Id=d.Id

     ELSE
       insert into OrderAudit 
  (Id, OrderStatus, CustomerId, FreightAmount, SubTotal, TotalDue, PaymentDate,ShippedDate,CancelDate,RecordCreated,RecordModified,AuditType, UpdatedBy, UpdatedOn )
  select d.Id, d.OrderStatus, d.CustomerId, d.FreightAmount, d.SubTotal, d.TotalDue, d.PaymentDate,d.ShippedDate,d.CancelDate,d.RecordCreated,d.RecordModified,@AuditType,SUSER_SNAME(), getdate() 
  from  deleted d;

  END

END