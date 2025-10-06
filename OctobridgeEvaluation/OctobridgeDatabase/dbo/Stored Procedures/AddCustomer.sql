CREATE PROCEDURE [dbo].[AddCustomer]
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
BEGIN
    INSERT INTO [dbo].[Customer]
               ([FirstName],[LastName],[MiddleName],[AddressLine1],[AddressLine2],[City],[PostalCode],[Telephone],[Email],[RecordCreated],[RecordModified])
    VALUES
               (@FirstName,@LastName,@MiddleName,@AddressLine1,@AddressLine2,@City,@PostalCode,@Telephone,@Email,DEFAULT,DEFAULT);

    RETURN 0;
END