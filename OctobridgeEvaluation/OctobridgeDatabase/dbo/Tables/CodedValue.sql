CREATE TABLE [dbo].[CodedValue]
(
	[GroupName]   nvarchar(20) NOT NULL,
	[Value]       nvarchar(100) NOT NULL,
	[Description] nvarchar(200) NULL
)
GO

ALTER TABLE [dbo].[CodedValue] ADD  CONSTRAINT [PK_CodedValue] PRIMARY KEY CLUSTERED ([GroupName],[Value]);
GO