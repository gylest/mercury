CREATE TABLE [dbo].[Attachment]
(
	[Id]            INT IDENTITY(1,1)  NOT NULL,
	[Filename]      NVARCHAR(255)      NOT NULL,
	[Filetype]      NVARCHAR(100)      NOT NULL,
	[Length]        BIGINT             NOT NULL,
	[Filedata]      VARBINARY (MAX)    NOT NULL,
	[RecordCreated] DATETIME2(7)       NOT NULL)
GO

ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [PK_Attachment_id] PRIMARY KEY CLUSTERED ([Id]);
GO

ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_RecordCreated]  DEFAULT (SYSDATETIME()) FOR [RecordCreated];
GO
