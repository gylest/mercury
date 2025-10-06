CREATE TABLE [dbo].[Product]
(
    [ProductId]         INT              NOT NULL IDENTITY(1,1),
    [Name]              [dbo].[NameType] NOT NULL,
    [ProductNumber]     NVARCHAR(50)     NOT NULL,
    [ProductCategoryId] INT              NOT NULL,
    [Cost]              MONEY            NOT NULL,
    [RecordCreated]     DATETIME2(7)     NOT NULL,
    [RecordModified]    DATETIME2(7)     NOT NULL
)
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductId]);
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_RecordModified]  DEFAULT (SYSDATETIME()) FOR [RecordModified];
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_RecordCreated]  DEFAULT (SYSDATETIME()) FOR [RecordCreated];
GO

ALTER TABLE [dbo].[Product]  ADD  CONSTRAINT [FK_ProductCategory_Product] FOREIGN KEY([ProductCategoryId]) REFERENCES [dbo].[ProductCategory] ([ProductCategoryId]);
GO

ALTER TABLE [dbo].[Product] ADD CONSTRAINT [UN_Product_ProductNumber] UNIQUE (ProductNumber);
GO