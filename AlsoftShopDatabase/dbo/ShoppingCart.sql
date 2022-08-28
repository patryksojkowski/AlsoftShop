CREATE TABLE [dbo].[ShoppingCart]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[ProductId] INT NOT NULL,
	[Quantity] INT NOT NULL,
	[Subtotal] MONEY NOT NULL,
	[Discount] MONEY NOT NULL,
	[Total] MONEY NOT NULL,
	FOREIGN KEY (ProductId) REFERENCES Product(Id)
)
