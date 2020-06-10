USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyDataUpdate
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[ShopifyDataUpdate]
	@ShopifyDataId int
AS

UPDATE ShopifyData SET EventType = 'Update' where Id = @ShopifyDataId

GO
