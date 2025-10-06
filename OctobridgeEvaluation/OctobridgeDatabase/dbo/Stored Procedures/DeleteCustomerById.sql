CREATE PROCEDURE [dbo].[DeleteCustomerById]
	@Id int
AS
DELETE FROM [dbo].[Customer]
      WHERE [Id]=@Id
RETURN 0
