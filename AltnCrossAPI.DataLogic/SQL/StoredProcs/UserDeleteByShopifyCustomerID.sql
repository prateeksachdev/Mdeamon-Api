USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: UserDeleteByShopifyCustomerID
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[UserDeleteByShopifyCustomerID]
	@ShopifyCustomerID bigint = 0
AS

DELETE FROM [Users] WHERE ShopifyCustomerID = @ShopifyCustomerID

GO
