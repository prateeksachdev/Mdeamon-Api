USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: UserShopifyCustomerIDUpdate
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[UserShopifyCustomerIDUpdate]
	@ShopifyCustomerID bigint,
	@UserID varchar(255)
AS

UPDATE [Users] SET ShopifyCustomerID = @ShopifyCustomerID WHERE UserID = @UserID

GO
