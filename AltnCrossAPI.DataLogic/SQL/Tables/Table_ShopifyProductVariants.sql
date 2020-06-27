USE [md_db]
GO

CREATE TABLE [dbo].[ShopifyProductVariants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShopifyId] [bigint] NULL,
	[ProductId] [bigint] NULL,
	[CompareAtPrice] [decimal](18, 2) NULL,
	[Price] [decimal](18, 2) NULL,
	[SKU] [varchar](100) NULL,
	[Title] [nvarchar](255) NULL,
	[Weight] [decimal](18, 2) NULL,
	[WeightUnit] [nvarchar](100) NULL,
	[InventoryQuantity] [int] NULL,
	[InventoryItemId] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductVariants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ShopifyProductVariants]  WITH CHECK ADD  CONSTRAINT [FK_ShopifyProductVariants_ShopifyProduct] FOREIGN KEY([ProductId])
REFERENCES [dbo].[ShopifyProduct] ([ShopifyId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ShopifyProductVariants] CHECK CONSTRAINT [FK_ShopifyProductVariants_ShopifyProduct]
GO


