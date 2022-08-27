CREATE TABLE [dbo].[Inventory]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProductId] UNIQUEIDENTIFIER NOT NULL, 
    [Stock] INT NULL, 
    CONSTRAINT [FK_Inventory_ToProductsTable] FOREIGN KEY (ProductId) REFERENCES [Products]([Id])
)
