USE [md_db]
GO

CREATE PROCEDURE [dbo].[ShopifyProductVariantCountUpdate]
	@ShopifyId bigint,
	@Increment int
AS

UPDATE 
	ShopifyProduct 
SET 
	VariantsCount = COALESCE(VariantsCount, 0) + @Increment, 
	UpdatedOn = GETDATE() 
WHERE 
	ShopifyId = @ShopifyId


