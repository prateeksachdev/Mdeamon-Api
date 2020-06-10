USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyOrderLineItemsDelete
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[ShopifyOrderLineItemsDelete]
	@WhereClause nvarchar(1000) = ' Where  Id  <> 0 '
AS


	DECLARE @SQL nvarchar(500)
	SET @SQL ='DELETE FROM [dbo].[ShopifyOrderLineItems] '
	SET @SQL = @SQL + @WhereClause
		
	EXEC(@SQL)

GO
