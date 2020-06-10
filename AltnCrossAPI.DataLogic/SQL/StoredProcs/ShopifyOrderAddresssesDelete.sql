USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyOrderAddresssesDelete
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[ShopifyOrderAddresssesDelete]
	@WhereClause nvarchar(1000) = ' Where  Id  <> 0 '
AS


	DECLARE @SQL nvarchar(500)
	SET @SQL ='DELETE FROM [dbo].[ShopifyOrderAddress] '
	SET @SQL = @SQL + @WhereClause
		
	EXEC(@SQL)

GO
