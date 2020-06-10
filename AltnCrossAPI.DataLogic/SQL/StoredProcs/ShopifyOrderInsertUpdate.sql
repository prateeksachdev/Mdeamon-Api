USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyOrderInsertUpdate
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

--exec [dbo].[ShopifyOrderInsertUpdate] 2143959547940,1025,135,'neha@mobikasa.com','2020-05-19 06:33:44.313','2020-05-19 06:33:44.313','2020-05-19 06:33:44.313','da729c65758f887a37d862c860bda2d9',
--'94944989c5b1c191a86828d410dddef9', '', 15.58,3299984.42, 15.58,0.00,'pending','manual','USD', -9223372036854775808,580111,'103.199.115.241','https://mdaemon.myshopify.com/10326736932/orders/da729c65758f887a37d862c860bda2d9/authenticate?key=fb3b2f84695b15bd36e4a1be0edc4304'
CREATE PROCEDURE [dbo].[ShopifyOrderInsertUpdate]
	@ShopifyId bigint, 
	@OrderNumber int, 
	@ShopifyDataId int, 
	@Email varchar(100), 
	@CreatedOn datetime, 
	@UpdatedOn datetime, 
	@ProcessedOn datetime, 
	@Token nvarchar(100), 
	@CheckoutToken nvarchar(100), 
	@Gateway nvarchar(100), 
	@TotalPrice decimal(18, 2), 
	@TotalDiscount decimal(18, 2), 
	@SubTotalPrice decimal(18, 2), 
	@TotalTax decimal(18, 2), 
	@FinancialStatus varchar(10), 
	@ProcessingMethod varchar(25), 
	@Currency varchar(5), 
	@CheckoutId bigint, 
	@AppId bigint, 
	@BrowserIP varchar(30), 
	@OrderStatusUrl varchar(500) 
AS

IF NOT EXISTS( SELECT 1 FROM ShopifyOrder where ShopifyId = @ShopifyId) BEGIN

INSERT INTO [dbo].[ShopifyOrder] (
	[ShopifyId],
	[OrderNumber],
	[ShopifyDataId],
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
) VALUES (
	@ShopifyId,
	@OrderNumber,
	@ShopifyDataId,
	@Email,
	@CreatedOn,
	@UpdatedOn,
	@ProcessedOn,
	@Token,
	@CheckoutToken,
	@Gateway,
	@TotalPrice,
	@TotalDiscount,
	@SubTotalPrice,
	@TotalTax,
	@FinancialStatus,
	@ProcessingMethod,
	@Currency,
	@CheckoutId,
	@AppId,
	@BrowserIP,
	@OrderStatusUrl
)


END
ELSE
BEGIN
UPDATE [dbo].[ShopifyOrder] SET
	[OrderNumber] = @OrderNumber,
	[ShopifyDataId] = @ShopifyDataId,
	[Email] = @Email,
	[CreatedOn] = @CreatedOn,
	[UpdatedOn] = @UpdatedOn,
	[ProcessedOn] = @ProcessedOn,
	[Token] = @Token,
	[CheckoutToken] = @CheckoutToken,
	[Gateway] = @Gateway,
	[TotalPrice] = @TotalPrice,
	[TotalDiscount] = @TotalDiscount,
	[SubTotalPrice] = @SubTotalPrice,
	[TotalTax] = @TotalTax,
	[FinancialStatus] = @FinancialStatus,
	[ProcessingMethod] = @ProcessingMethod,
	[Currency] = @Currency,
	[CheckoutId] = @CheckoutId,
	[AppId] = @AppId,
	[BrowserIP] = @BrowserIP,
	[OrderStatusUrl] = @OrderStatusUrl
WHERE
	[ShopifyId] = @ShopifyId
	
exec [ShopifyDataUpdate] @ShopifyDataId

END

GO
