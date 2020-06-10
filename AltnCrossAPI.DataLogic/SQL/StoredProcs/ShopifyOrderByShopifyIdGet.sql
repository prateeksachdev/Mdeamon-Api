USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyOrderByShopifyIdGet
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[ShopifyOrderByShopifyIdGet]
	@ShopifyId bigint
AS

SELECT
	[Id],
	[ShopifyId],
	[OrderNumber],
	[Email],
	[CreatedOn],
	[UpdatedOn],
	[ProcessedOn],
	[Token],
	[CheckoutToken],
	[Gateway],
	[TotalPrice],
	[TotalDiscount],
	[SubTotalPrice],
	[TotalTax],
	[FinancialStatus],
	[ProcessingMethod],
	[Currency],
	[CheckoutId],
	[AppId],
	[BrowserIP],
	[OrderStatusUrl]
FROM
	[dbo].[ShopifyOrder]
WHERE
	[ShopifyId] = @ShopifyId

GO
