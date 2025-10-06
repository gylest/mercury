CREATE PROCEDURE [dbo].[UpdateProductById]
    @ProductId          INT,
    @Name               NVARCHAR(100),
    @ProductNumber      NVARCHAR(50),
    @ProductCategoryId  INT,
    @Cost               MONEY
AS
BEGIN TRY
    UPDATE [dbo].[Product]
    SET [Name]               = @Name
       ,[ProductNumber]      = @ProductNumber
       ,[ProductCategoryId]  = @ProductCategoryId
       ,[Cost]               = @Cost
       ,[RecordModified]     = DEFAULT
     WHERE [ProductId] = @ProductId

     SELECT [ProductId]
           ,[Name]
           ,[ProductNumber]
           ,[ProductCategoryId]
           ,[Cost]
           ,[RecordCreated]
           ,[RecordModified]
     FROM [dbo].[Product]
     WHERE [ProductId] = @ProductId;

END TRY
BEGIN CATCH
    THROW;
END CATCH;

RETURN 0