﻿CREATE TABLE [dbo].[sales]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProductId] UNIQUEIDENTIFIER NOT NULL, 
    [SalesPrice] MONEY NOT NULL, 
    [TimeOfSale] DATETIME NULL
)