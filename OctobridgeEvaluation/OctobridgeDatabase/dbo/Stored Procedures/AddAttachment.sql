CREATE PROCEDURE [dbo].[AddAttachment]
	@filename   NVARCHAR(255),
	@filetype   NVARCHAR(100),
	@filelength BIGINT,
	@filedata   VARBINARY(MAX),
	@Id         INTEGER       OUTPUT,
    @dt         DATETIME2(7)  OUTPUT
AS
BEGIN
    SET @dt = SYSDATETIME();

    INSERT INTO [dbo].[Attachment]([Filename],[Filetype],[Length],[Filedata],[RecordCreated])
    VALUES                        (@filename, @filetype, @filelength, @filedata, @dt);

    SET @Id = SCOPE_IDENTITY();

    RETURN 0 
END
