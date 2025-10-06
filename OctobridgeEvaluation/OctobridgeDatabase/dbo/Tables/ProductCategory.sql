CREATE TABLE [dbo].[ProductCategory]
(
    [ProductCategoryId] INT           NOT NULL IDENTITY(1,1),
    [Name]              NVARCHAR(50)  NOT NULL,
    [Description]       NVARCHAR(100) NOT NULL,
    [RecordCreated]     DATETIME2(7)  NOT NULL,
    [RecordModified]    DATETIME2(7)  NOT NULL
)
GO

ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED ([ProductCategoryId]);
GO

ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_RecordCreated]  DEFAULT (SYSDATETIME()) FOR [RecordCreated];
GO

ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_RecordModified]  DEFAULT (SYSDATETIME()) FOR [RecordModified];
GO