USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyOrderLineItemsByOrderIdGet
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[ShopifyOrderLineItemsByOrderIdGet]
	@OrderId bigint
AS

SELECT
	[Id],
	[ShopifyId],
	[OrderId],
	[VariantId],
	[Title],
	[Quantity],
	[FulfillableQuantity],
	[SKU],
	[Vendor],
	[ProductId],
	[Price],
	[TotalDiscount],
	[TaxCode],
	[DateAdded],
	[DateUpdated]
FROM
	[dbo].[ShopifyOrderLineItems]
WHERE
	[OrderId] = @OrderId

GO
