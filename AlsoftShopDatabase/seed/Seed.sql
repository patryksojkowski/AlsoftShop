INSERT INTO Product (Id, Name, Price, Unit, AdditionalProperties)
VALUES
(1, N'Soup', 0.65, 'tin', N'{"flavour":"chicken"}' ),
(2, N'Bread', 0.80, 'loaf', N'{"calories":"251 kcal"}'),
(3, N'Roll', 0.30, 'item', N'{"calories":"80 kcal"}'),
(4, N'Full-cream milk', 1.60, 'bottle', N'{"fat":"3.2 %"}'),
(5, N'Low-fat milk', 1.30, 'bottle', N'{"fat":"1.5%"}'),
(6, N'Apples', 1.00, 'bag', N'{"origin":"Poland"}')


INSERT INTO Discount (DiscountedProductId, DiscountTriggerProductId, DiscountTriggerProductCount, DiscountPercentage)
VALUES
(6, 6, 1, 0.1), /* apples have 10% discount */
(3, 1, 2, 0.5), /* 50% on roll for 2 tins of soup */
(2, 4, 1, 0.2), /* 20% percent on bread for full milk */
(2, 5, 1, 0.2), /* 20% on bread for low milk */
(4, 2, 1, 0.2), /* 20% on full milk for bread */
(5, 2, 1, 0.2) /* 20% on full milk for bread */