CREATE PROCEDURE [dbo].[DeleteAttachmentById]
    @Id int
AS
DELETE FROM [dbo].[Attachment]
      WHERE [Id]=@Id
RETURN 0
