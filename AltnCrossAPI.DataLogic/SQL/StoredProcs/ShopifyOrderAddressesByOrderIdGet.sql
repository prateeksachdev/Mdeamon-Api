USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyOrderAddressesByOrderIdGet
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

------------------------------------------------------------------------------------------------------------------------
-- Copyright:   EgySoft.
------------------------------------------------------------------------------------------------------------------------

Create PROCEDURE [dbo].[ShopifyOrderAddressesByOrderIdGet]
	@OrderId bigint
AS

SELECT
	[Id],
	[FirstName],
	[LastName],
	[Address],
	[Phone],
	[City],
	[Zip],
	[Province],
	[Country],
	[Latitude],
	[Longitude],
	[CountryCode],
	[DateAdded],
	[DateUpdated],
	[OrderId],
	[AddressType]
FROM
	[dbo].[ShopifyOrderAddress]
WHERE
	[OrderId] = @OrderId


GO
