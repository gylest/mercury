CREATE PROCEDURE [dbo].[UpdateCustomerByID]
    @Id               INT,
    @FirstName        NVARCHAR(100),
    @LastName         NVARCHAR(100),
    @MiddleName       NVARCHAR(50),
    @AddressLine1     NVARCHAR(60),
    @AddressLine2     NVARCHAR(60),
    @City             NVARCHAR(30),
    @PostalCode       NVARCHAR(15),
    @Telephone        NVARCHAR(25),
    @Email            NVARCHAR(25)
AS
BEGIN TRY

    UPDATE [dbo].[Customer]
    SET [FirstName]      = @FirstName
        ,[LastName]       = @LastName
        ,[MiddleName]     = @MiddleName
        ,[AddressLine1]   = @AddressLine1
        ,[AddressLine2]   = @AddressLine2
        ,[City]           = @City
        ,[PostalCode]     = @PostalCode
        ,[Telephone]      = @Telephone
        ,[Email]          = @Email
        ,[RecordModified] = DEFAULT
    WHERE [Id] = @Id

    SELECT  [Id]
           ,[FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[AddressLine1]
           ,[AddressLine2]
           ,[City]
           ,[PostalCode]
           ,[Telephone]
           ,[Email]
           ,[RecordCreated]
           ,[RecordModified]
    FROM [dbo].[Customer]
    WHERE [Id] = @Id;

END TRY
BEGIN CATCH
    THROW;
END CATCH;

RETURN 0
