/*
Post-Deployment Script Template
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.
 Use SQLCMD syntax to include a file in the post-deployment script.
 Example:      :r .\myfile.sql
 Use SQLCMD syntax to reference a variable in the post-deployment script.
 Example:      :setvar TableName MyTable
               SELECT * FROM [$(TableName)]
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[ProductCategory] ON;
INSERT INTO [dbo].[ProductCategory]
            ([ProductCategoryId],[Name],[Description],[RecordCreated],[RecordModified])
VALUES
            (1,  'Stationary',    'Paper, Pens, inks', DEFAULT, DEFAULT),
            (2,  'Movies',        'DVDs, Blu-rays and 4K discs', DEFAULT, DEFAULT),
            (3,  'Home',          'Home products', DEFAULT, DEFAULT),
            (4,  'Computer',      'Computer products and accessories', DEFAULT, DEFAULT),
            (5,  'Book',          'Books and magazines', DEFAULT, DEFAULT),
            (6,  'Games',         'Computer games', DEFAULT, DEFAULT),
            (7,  'Music',         'CDs and Vinyl', DEFAULT, DEFAULT);
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF;

SET IDENTITY_INSERT [dbo].[Product] ON;
INSERT INTO [dbo].[Product]
            ([ProductId],[Name], [ProductNumber], [ProductCategoryId], [Cost],[RecordCreated],[RecordModified])
VALUES
            (1,'Ball Point Pen',  'BP-1001-01', 1, 0.99, DEFAULT, DEFAULT),
            (2,'Avengers Endgame','AE-2004-03', 2, 24.99, DEFAULT, DEFAULT),
            (3,'Steam Iron',      'SI-4010-70', 3, 55.00, DEFAULT, DEFAULT),
            (4,'Creative Sound Blaster PCIe Gaming Sound Card','SB1740', 4, 140.00, DEFAULT, DEFAULT),
            (5,'Harry Potter and the Philosopher''s Stone', '978-1408855652', 5, 55.00, DEFAULT, DEFAULT),
            (6,'Shadow of the Tomb Raider (Xbox One)', 'B07BHB1VX5', 6, 10.99, DEFAULT, DEFAULT),
            (7,'I''ve Been Expecting You', 'B0000676WX', 7, 3.50, DEFAULT, DEFAULT),
            (8,'Tefal Loft KO250840 Kettle', 'KO250840', 3, 45.00, DEFAULT, DEFAULT),
            (9,'Just Stationery 12 inch plastic Shatterproof Ruler', '0197640', 1, 00.87, DEFAULT, DEFAULT),
            (10,'For a Few Dollars More', 'B07LD4P3BG', 2, 7.99, DEFAULT, DEFAULT);
GO
SET IDENTITY_INSERT [dbo].[Product] OFF;

SET IDENTITY_INSERT [dbo].[Customer] ON;
INSERT INTO [dbo].[Customer]
           ([Id],[FirstName] ,[LastName] ,[MiddleName] ,[AddressLine1] ,[AddressLine2],[City],[PostalCode],[Telephone],[Email],[RecordCreated],[RecordModified])
VALUES
           (1,'Tony','Gyles','Peter','9 Turnberry Drive','Beggarwood','Basingstoke','RG22 4WT','01256468552','tonygyles@btinternet.com',DEFAULT,DEFAULT),
           (2,'Jayne','Hawley','','35 Stafford Drive','Littleover','Derby','DE23 3W9','01332668775','jaynehawley27@gmail.com',DEFAULT,DEFAULT),
           (3,'Steve','Gyles','Paul','12 Ringshall Gardens','','Bramley','RG26 5BW','','steve.gyles@ntlworld.com',DEFAULT,DEFAULT),
           (4,'Tom','Sheppard','John','16 Happenstance Lance','','Glasgow','GW1 2TF','','tomshep1@btinternet.com',DEFAULT,DEFAULT),
           (5,'Paula','Gyles','','61 Copland Close','','Basingstoke','RG22 4LB','','pgyles@gmail.com',DEFAULT,DEFAULT);
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF;

INSERT INTO [dbo].[CodedValue]
            ([GroupName],[Value],[Description])
VALUES
            ('OrderStatus',  'Open',      'Order has been created'),
            ('OrderStatus',  'Paid',      'Order has been paid for'),
            ('OrderStatus',  'Rejected',  'Order has been rejected due to non-payment'),
            ('OrderStatus',  'Shipped',   'Order has shipped'),
            ('OrderStatus',  'Cancelled', 'Order has been cancelled by customer');
GO

SET IDENTITY_INSERT [dbo].[Order] ON;
INSERT INTO [dbo].[Order]
           ([Id],[OrderStatus],[CustomerId],[FreightAmount],[SubTotal],[TotalDue],[PaymentDate],[ShippedDate],[CancelDate],[RecordCreated],[RecordModified])
VALUES
           (1, 'Open', 1, 4.99, 55.00, 59.99,NULL,NULL,NULL,DEFAULT,DEFAULT),
           (2, 'Open', 2, 0.00, 164.99, 164.99,NULL,NULL,NULL,DEFAULT,DEFAULT),
           (3, 'Open', 3, 4.99, 1.98, 6.97,NULL,NULL,NULL,DEFAULT,DEFAULT)
GO
SET IDENTITY_INSERT [dbo].[Order] OFF;

INSERT INTO [dbo].[OrderDetail]
           ([OrderId],[ProductId],[UnitPrice],[Quantity],[RecordCreated],[RecordModified])
VALUES
           ( 1, 3, 55.00, 1, DEFAULT, DEFAULT),
           ( 2, 4, 140.00, 1, DEFAULT, DEFAULT),
           ( 2, 2, 24.99, 1, DEFAULT, DEFAULT),
           ( 3, 1, 0.99, 2, DEFAULT, DEFAULT);
GO

INSERT INTO [dbo].[Payment]
           ([OrderId],[TransactionId],[Gateway],[CardType],[CardNumber],[ExpiryDate],[Amount],[RecordCreated],[RecordModified])
VALUES
           (1, '1234500001', 'Mastercard','Visa','1111222233334444', '1222', 59.99, DEFAULT,DEFAULT),
           (2, '1234500002', 'Mastercard','Mastercard','5555666677778888', '0623', 164.99, DEFAULT,DEFAULT),
           (3, '1234500003', 'Mastercard','Visa','1111222233334444', '1222', 6.97, DEFAULT,DEFAULT);
GO