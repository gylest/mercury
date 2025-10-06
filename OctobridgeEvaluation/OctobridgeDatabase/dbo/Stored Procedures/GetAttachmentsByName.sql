CREATE PROCEDURE [dbo].[GetAttachmentsByName]
    @filename  nvarchar(255)
AS
BEGIN

    DECLARE @searchFilename nvarchar(255);

    if( @filename is null) 
        set @searchFilename = '%';
    else
        set @searchFilename = @filename + '%';

    SELECT [Id],[Filename],[Filetype],[Length],[RecordCreated]
    FROM   [dbo].[Attachment]
    WHERE  ([Filename] like @searchFilename )

    RETURN 0 

END
