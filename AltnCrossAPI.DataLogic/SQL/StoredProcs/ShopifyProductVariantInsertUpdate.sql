USE [md_db]
GO


CREATE PROCEDURE [dbo].[ShopifyProductVariantInsertUpdate]
	@ShopifyId bigint, 
	@ProductId bigint,
	@Barcode nvarchar(20) = null,
	@CompareAtPrice decimal(18,2) = null,
	@Price decimal(18,2),
	@SKU varchar(100) = null,
	@Title nvarchar(255), 
	@Weight decimal(18,2) = null,
	@WeightUnit varchar(100) = null,
	@InventoryQuantity int = null, 
	@InventoryItemId bigint = null
AS

IF NOT EXISTS( SELECT 1 FROM ShopifyProductVariants where ShopifyId = @ShopifyId) BEGIN

INSERT INTO [dbo].[ShopifyProductVariants] (
	[ShopifyId],
	[ProductId],
	[Barcode],
	[CompareAtPrice],
	[Price],
	[SKU],
	[Title],
	[Weight],
	[WeightUnit],
	[InventoryQuantity],
	[InventoryItemId],
	[CreatedOn],
	[UpdatedOn]
) VALUES (
	@ShopifyId,
	@ProductId,
	@Barcode,
	@CompareAtPrice,
	@Price,
	@SKU,
	@Title,
	@Weight,
	@WeightUnit,
	@InventoryQuantity,
	@InventoryItemId,
	GETDATE(),
	GETDATE()
)

exec ShopifyProductVariantCountUpdate @ProductId, 1

END
ELSE
BEGIN

UPDATE [dbo].[ShopifyProductVariants] SET
	[Barcode] = @Barcode,
	[CompareAtPrice] = @CompareAtPrice,
	[Title] = @Title,
	[UpdatedOn] = GETDATE(),
	[Price] = @Price,
	[SKU] = @SKU,
	[Weight] = @Weight,
	[WeightUnit] = @WeightUnit,
	[InventoryQuantity] = @InventoryQuantity,
	[InventoryItemId] = @InventoryItemId
WHERE
	[ShopifyId] = @ShopifyId

END

