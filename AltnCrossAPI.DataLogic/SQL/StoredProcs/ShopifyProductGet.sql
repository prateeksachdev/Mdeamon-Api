USE [md_db]
GO


CREATE PROCEDURE [dbo].[ShopifyProductGet]
	@ProductId bigint = 0
AS

SELECT
	*
FROM
	[dbo].[ShopifyProduct]
WHERE
	[ShopifyId] = @ProductId