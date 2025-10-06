CREATE PROCEDURE [dbo].[AddProduct]
    @Name                NVARCHAR(100),
    @ProductNumber       NVARCHAR(100),
    @ProductCategoryId   INT,
    @Cost                MONEY
AS
BEGIN
    INSERT INTO [dbo].[Product]
               ([Name], [ProductNumber], [ProductCategoryId], [Cost], [RecordCreated], [RecordModified])
    VALUES
               (@Name,@ProductNumber,@ProductCategoryId,@Cost,DEFAULT,DEFAULT);

    RETURN 0;
END