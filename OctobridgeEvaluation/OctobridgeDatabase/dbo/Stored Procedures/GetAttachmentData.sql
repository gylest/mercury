CREATE PROCEDURE [dbo].[GetAttachmentData]
    @id int
AS
BEGIN
    SELECT [Filename],[Filetype],[Length],[Filedata],[RecordCreated]
    FROM   [dbo].[Attachment]
    WHERE  [Id] = @id;

    RETURN 0 

END
