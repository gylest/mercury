CREATE TABLE [dbo].[OrderDetail]
(
    [LineId]          INT           NOT NULL IDENTITY(1,1),
    [OrderId]         INT           NOT NULL,
    [ProductId]       INT           NOT NULL,
    [UnitPrice]       MONEY         NOT NULL,
    [Quantity]        INT           NOT NULL,
    [RecordCreated]   DATETIME2(7)  NOT NULL,
    [RecordModified]  DATETIME2(7)  NOT NULL
)
GO

ALTER TABLE [dbo].[OrderDetail]  ADD  CONSTRAINT [FK_Product_Order] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Product] ([ProductId]);
GO

ALTER TABLE [dbo].[OrderDetail]  ADD  CONSTRAINT [FK_OrderDetail_Order_Id] FOREIGN KEY([OrderId]) REFERENCES [dbo].[Order] ([Id]);
GO

ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_RecordCreated]  DEFAULT (SYSDATETIME()) FOR [RecordCreated];
GO

ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_RecordModified]  DEFAULT (SYSDATETIME()) FOR [RecordModified];
GO