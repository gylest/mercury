CREATE PROCEDURE [dbo].[GetProductById]
	@id  int
AS
BEGIN

    SELECT [ProductId], [Name], [ProductNumber], [ProductCategoryId], [Cost], [RecordCreated], [RecordModified]
    FROM [dbo].[Product]
    WHERE [ProductId]=@id

    RETURN 0

END
