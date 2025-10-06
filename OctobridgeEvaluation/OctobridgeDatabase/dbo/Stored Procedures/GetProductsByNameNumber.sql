CREATE PROCEDURE [dbo].[GetProductsByNameNumber]
    @name  NVARCHAR(100),
    @productNumber NVARCHAR(50)
AS
BEGIN
    DECLARE @searchName NVARCHAR(100);
    DECLARE @searchProductNumber NVARCHAR(50);

    if( @name is null) 
        set @searchName = '%';
    else
        set @searchName = @name + '%';

    if( @productNumber is null) 
        set @searchProductNumber = '%';
    else
        set @searchProductNumber = @productNumber + '%';

    SELECT [ProductId], [Name], [ProductNumber], [ProductCategoryId], [Cost], [RecordCreated], [RecordModified]
    FROM [dbo].[Product]
    where ([Name] like @searchName ) and ([ProductNumber] like @searchProductNumber)

    RETURN 0

END
