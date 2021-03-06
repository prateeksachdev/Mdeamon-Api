USE [md_db]
GO


CREATE PROCEDURE [dbo].[ShopifyProductVariantIdGet]
	@ProductId bigint,
	@Price decimal(18,2)
AS

SELECT
	[ShopifyId]
FROM
	[dbo].[ShopifyProductVariants]
WHERE
	[ProductId] = @ProductId AND [Price] = @Price AND [Barcode] <> 'custom'