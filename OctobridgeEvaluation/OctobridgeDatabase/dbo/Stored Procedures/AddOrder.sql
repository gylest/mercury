CREATE PROCEDURE [dbo].[AddOrder]
    @OrderStatus    NVARCHAR (50),
    @CustomerId     INT,
    @FreightAmount  MONEY,
    @SubTotal       MONEY,
    @TotalDue       MONEY
AS
BEGIN

    INSERT INTO [dbo].[Order]
    ([OrderStatus],[CustomerId],[FreightAmount],[SubTotal],[TotalDue],[PaymentDate],[ShippedDate],[CancelDate],[RecordCreated],[RecordModified])
    VALUES
    (@OrderStatus,@CustomerId,@FreightAmount,@SubTotal,@TotalDue,NULL,NULL,NULL,DEFAULT,DEFAULT)

    RETURN 0
END