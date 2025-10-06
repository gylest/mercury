CREATE PROCEDURE [dbo].[DeleteOrderById]
	@Id int
AS
DELETE FROM [dbo].[Order]
      WHERE [Id]=@Id
RETURN 0
