CREATE TABLE [dbo].[OrderAudit]
(
	[AuditId]        INT           NOT NULL IDENTITY(1,1),
    [Id]             INT           NOT NULL,
    [OrderStatus]    NVARCHAR (50) NOT NULL,
    [CustomerId]     INT           NOT NULL,
    [FreightAmount]  MONEY         NOT NULL,
    [SubTotal]       MONEY         NOT NULL,
    [TotalDue]       MONEY         NOT NULL,
    [PaymentDate]    DATETIME2(7)      NULL,
    [ShippedDate]    DATETIME2(7)      NULL,
    [CancelDate]     DATETIME2(7)      NULL,
    [RecordCreated]  DATETIME2(7)  NOT NULL,
    [RecordModified] DATETIME2(7)  NOT NULL,
	[AuditType]      NVARCHAR(2)   NOT NULL,
	[UpdatedBy]      NVARCHAR(100) NOT NULL,
    [UpdatedOn]      DATETIME2(7)  NOT NULL
)
GO

ALTER TABLE [dbo].[OrderAudit] ADD  CONSTRAINT [PK_OrderAudit] PRIMARY KEY CLUSTERED ([AuditId]);
GO

ALTER TABLE [dbo].[OrderAudit] ADD  CONSTRAINT [DF_OrderAudit_UpdatedBy]  DEFAULT (SYSDATETIME()) FOR [UpdatedBy];
GO

ALTER TABLE [dbo].[OrderAudit] ADD  CONSTRAINT [DF_OrderAudit_UpdatedOn]  DEFAULT (SYSDATETIME()) FOR [UpdatedOn];
GO