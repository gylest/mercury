CREATE TABLE [dbo].[Customer]
(
    [Id] INT NOT NULL IDENTITY(1,1),
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [MiddleName] NVARCHAR(50) NULL,
    [AddressLine1] NVARCHAR(60) NOT NULL,
    [AddressLine2] NVARCHAR(60) NULL,
    [City] NVARCHAR(30) NOT NULL,
    [PostalCode] NVARCHAR(15) NOT NULL,
    [Telephone] NVARCHAR(25) NOT NULL,
    [Email] NVARCHAR(25) NOT NULL,
    [RecordCreated]   DATETIME2(7)  NOT NULL,
    [RecordModified] DATETIME2(7) NOT NULL
)
GO

ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id]);
GO

ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_RecordCreated]  DEFAULT (SYSDATETIME()) FOR [RecordCreated];
GO

ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_RecordModified]  DEFAULT (SYSDATETIME()) FOR [RecordModified];
GO