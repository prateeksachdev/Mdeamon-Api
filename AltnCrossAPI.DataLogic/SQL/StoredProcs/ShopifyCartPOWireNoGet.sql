USE [md_db]
GO

CREATE PROCEDURE [dbo].[ShopifyCartPOWireNoGet]
	@CartToken nvarchar(255)
AS

SELECT
	POWireNo
FROM
	[dbo].[ShopifyCartPOWireNo]
WHERE
	[CartToken] = @CartToken
