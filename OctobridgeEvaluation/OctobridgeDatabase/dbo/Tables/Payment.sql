CREATE TABLE [dbo].[Payment]
(
    [Id]             INT           NOT NULL IDENTITY(1,1),
    [OrderId]        INT           NOT NULL,
    [TransactionId]  NVARCHAR (50) NOT NULL,
    [Gateway]        NVARCHAR(20)  NOT NULL,
    [CardType]       NVARCHAR(20)  NOT NULL,
    [CardNumber]     NVARCHAR(20)  NOT NULL,
    [ExpiryDate]     NVARCHAR(4)   NOT NULL,
    [Amount]         MONEY         NOT NULL,
    [RecordCreated]  DATETIME2(7)  NOT NULL,
    [RecordModified] DATETIME2(7)  NOT NULL
)
GO

ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [FK_Payment_Order] FOREIGN KEY([OrderId]) REFERENCES [dbo].[Order] ([Id]);
GO

ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [DF_Payment_RecordCreated]  DEFAULT (SYSDATETIME()) FOR [RecordCreated];
GO

ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [DF_Payment_RecordModified]  DEFAULT (SYSDATETIME()) FOR [RecordModified];
GO

ALTER TABLE [dbo].[Payment] ADD CONSTRAINT [UN_Payment_TransactionId] UNIQUE (TransactionId);
GO
