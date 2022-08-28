CREATE TABLE [dbo].[Discount]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[DiscountedProductId] INT NOT NULL,
	[DiscountTriggerProductId] INT NOT NULL,
	[DiscountTriggerProductCount] INT NOT NULL,
	[DiscountPercentage] DECIMAL NOT NULL

	FOREIGN KEY ([DiscountedProductId]) REFERENCES Product(Id)
	FOREIGN KEY ([DiscountTriggerProductId]) REFERENCES Product(Id)
)
