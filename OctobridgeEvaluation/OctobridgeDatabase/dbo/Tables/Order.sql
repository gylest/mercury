CREATE TABLE [dbo].[Order]
(
    [Id]             INT           NOT NULL IDENTITY(1,1),
    [OrderStatus]    NVARCHAR (50) NOT NULL,
    [CustomerId]     INT           NOT NULL,
    [FreightAmount]  MONEY         NOT NULL,
    [SubTotal]       MONEY         NOT NULL,
    [TotalDue]       MONEY         NOT NULL,
    [PaymentDate]    DATETIME2(7)      NULL,
    [ShippedDate]    DATETIME2(7)      NULL,
    [CancelDate]     DATETIME2(7)      NULL,
    [RecordCreated]  DATETIME2(7)  NOT NULL,
    [RecordModified] DATETIME2(7)  NOT NULL
)
GO

ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id]);
GO

ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_RecordCreated]  DEFAULT (SYSDATETIME()) FOR [RecordCreated];
GO

ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_RecordModified]  DEFAULT (SYSDATETIME()) FOR [RecordModified];
GO

ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [FK_Order_Customer] FOREIGN KEY([CustomerId]) REFERENCES [dbo].[Customer] ([Id]);
GO