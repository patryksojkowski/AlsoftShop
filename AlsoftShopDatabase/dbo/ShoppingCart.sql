CREATE TABLE [dbo].[ShoppingCart]
(
	[Id] INT NOT NULL PRIMARY KEY,

	[Subtotal] MONEY NOT NULL,
	[Discount] MONEY NOT NULL,
	[Total] MONEY NOT NULL,
)
