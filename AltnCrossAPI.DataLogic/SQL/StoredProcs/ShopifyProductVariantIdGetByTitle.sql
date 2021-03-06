USE [md_db]
GO


CREATE PROCEDURE [dbo].[ShopifyProductVariantIdGetByTitle]
	@ProductId bigint,
	@VariantTitle nvarchar(255)
AS

SELECT
	[ShopifyId]
FROM
	[dbo].[ShopifyProductVariants]
WHERE
	[ProductId] = @ProductId AND [Title] = @VariantTitle
