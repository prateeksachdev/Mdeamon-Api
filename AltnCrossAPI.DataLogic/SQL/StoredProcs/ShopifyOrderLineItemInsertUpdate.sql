USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyOrderLineItemInsertUpdate
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[ShopifyOrderLineItemInsertUpdate]
	@ShopifyId bigint, 
	@OrderId bigint, 
	@VariantId bigint, 
	@Title varchar(200), 
	@Quantity int, 
	@FulfillableQuantity int, 
	@SKU varchar(100), 
	@Vendor varchar(100), 
	@ProductId bigint, 
	@Price decimal(18, 2), 
	@TotalDiscount decimal(18, 2), 
	@TaxCode varchar(30) = ''
AS

IF NOT EXISTS( SELECT 1 FROM ShopifyOrderLineItems where OrderId = @OrderId and ShopifyId = @ShopifyId) BEGIN

INSERT INTO [dbo].[ShopifyOrderLineItems] (
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
) VALUES (
	@ShopifyId,
	@OrderId,
	@VariantId,
	@Title,
	@Quantity,
	@FulfillableQuantity,
	@SKU,
	@Vendor,
	@ProductId,
	@Price,
	@TotalDiscount,
	@TaxCode,
	getdate(),
	getdate()
)


END
ELSE
BEGIN
UPDATE [dbo].[ShopifyOrderLineItems] SET
	[VariantId] = @VariantId,
	[Title] = @Title,
	[Quantity] = @Quantity,
	[FulfillableQuantity] = @FulfillableQuantity,
	[SKU] = @SKU,
	[Vendor] = @Vendor,
	[ProductId] = @ProductId,
	[Price] = @Price,
	[TotalDiscount] = @TotalDiscount,
	[TaxCode] = @TaxCode,
	[DateUpdated] = getdate()
WHERE
	[OrderId] = @OrderId and [ShopifyId] = @ShopifyId

END

GO
