CREATE PROCEDURE [dbo].[UpdateOrderByID]
    @Id               INT,
    @OrderStatus      NVARCHAR (50),
    @OrderDate        DATETIME2(7)
AS
BEGIN TRY
    DECLARE @PaymentDate     DATETIME2(7)
    DECLARE @ShippedDate     DATETIME2(7)
    DECLARE @CancelDate      DATETIME2(7)

    SELECT
      @PaymentDate =
      CASE   
         WHEN @OrderStatus = 'PAID' THEN @OrderDate
         ELSE [PaymentDate]
      END,
      @ShippedDate =
      CASE   
         WHEN @OrderStatus = 'SHIPPED' THEN @OrderDate
         ELSE [ShippedDate]
      END,
      @CancelDate =
      CASE   
         WHEN @OrderStatus = 'CANCELLED' THEN @OrderDate
         ELSE [CancelDate]
      END
     FROM [dbo].[Order]
     WHERE [Id] = @Id;

    UPDATE [dbo].[Order]
    SET [OrderStatus] = @OrderStatus
       ,[PaymentDate] = @PaymentDate
       ,[ShippedDate] = @ShippedDate
       ,[CancelDate]  = @CancelDate
       ,[RecordModified] = DEFAULT
    WHERE [Id] = @Id;

    SELECT [Id]
          ,[OrderStatus]
          ,[CustomerId]
          ,[FreightAmount]
          ,[SubTotal]
          ,[TotalDue]
          ,[PaymentDate]
          ,[ShippedDate]
          ,[CancelDate]
          ,[RecordCreated]
          ,[RecordModified]
     FROM [dbo].[Order]
     WHERE [Id] = @Id;

END TRY
BEGIN CATCH
    THROW;
END CATCH;

RETURN 0