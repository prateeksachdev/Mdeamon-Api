USE [md_db]
GO

CREATE PROCEDURE [dbo].[ShopifyProductDelete]
	@ShopifyId bigint
AS

DELETE FROM ShopifyProduct WHERE ShopifyId = @ShopifyId


