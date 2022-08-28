CREATE TABLE [dbo].[CartItem]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[ProductId] INT NOT NULL,
	[Quantity] INT NOT NULL,
	[ShoppingCartId] INT NOT NULL

	FOREIGN KEY (ProductId) REFERENCES Product(Id)
	FOREIGN KEY (ShoppingCartId) REFERENCES ShoppingCart(Id)
)
