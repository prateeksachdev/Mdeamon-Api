USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyDataInsert
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[ShopifyOrderLineItemRegKeyUpdate]
	@ShopifyId bigint, 
	@OrderId bigint, 
	@RegKey varchar(255)
AS

IF EXISTS( SELECT 1 FROM ShopifyOrderLineItems where OrderId = @OrderId and ShopifyId = @ShopifyId) BEGIN

UPDATE [dbo].[ShopifyOrderLineItems] SET
	[RegKey] = @RegKey,
	[DateUpdated] = getdate()
WHERE
	[OrderId] = @OrderId and [ShopifyId] = @ShopifyId


END
