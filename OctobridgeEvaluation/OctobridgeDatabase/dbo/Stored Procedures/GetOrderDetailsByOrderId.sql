CREATE PROCEDURE [dbo].[GetOrderDetailsByOrderId]
	@orderId  int
AS
BEGIN

	SELECT 	[LineId] , [OrderId], [ProductId] , [UnitPrice] , [Quantity] , [RecordCreated],[RecordModified] 
	FROM [dbo].[OrderDetail]
    WHERE [OrderId]=@orderId

	RETURN 0

END