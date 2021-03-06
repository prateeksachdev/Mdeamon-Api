USE [md_db]
GO


CREATE PROCEDURE [dbo].[ShopifyProductByParentProductGetLast]
	@ParentProductId bigint = 0
AS

SELECT
	*
FROM
	[dbo].[ShopifyProduct]
WHERE
	[ParentShopifyId] = @ParentProductId
ORDER BY
	[CreatedOn] DESC