USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: UserGetByShopifyCustomerID
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[UserGetByShopifyCustomerID]
	@ShopifyCustomerID bigint = 0
AS

SELECT * FROM [Users] WHERE ShopifyCustomerID = @ShopifyCustomerID

GO
