﻿CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(32) NOT NULL,
	[Price] MONEY NOT NULL,
	[Unit] NVARCHAR(16) NOT NULL,
	[AdditionalProperties] NVARCHAR(256)
)