CREATE PROCEDURE [dbo].[GetCustomersByName]
	@lastName  nvarchar(100),
	@firstName nvarchar(100)
AS
BEGIN
	DECLARE @searchLastName nvarchar(100);
	DECLARE @searchFirstName nvarchar(100);

	if( @lastName is null) 
		set @searchLastName = '%';
	else
		set @searchLastName = @lastName + '%';

	if( @firstName is null) 
		set @searchFirstName = '%';
	else
		set @searchFirstName = @firstName + '%';

	SELECT 	[Id] , [FirstName], [LastName] , [MiddleName] , [AddressLine1] , [AddressLine2] , [City] , [PostalCode], [Telephone], [Email], [RecordCreated],[RecordModified] 
	FROM [dbo].[Customer]
	where ([FirstName] like @searchFirstName ) and ([LastName] like @searchLastName)

	RETURN 0

END