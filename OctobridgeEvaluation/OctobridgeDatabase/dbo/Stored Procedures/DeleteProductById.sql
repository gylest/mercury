CREATE PROCEDURE [dbo].[DeleteProductById]
    @Id int
AS
DELETE FROM [dbo].[Product]
      WHERE [ProductId]=@Id
RETURN 0