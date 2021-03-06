USE [md_db]
GO
/****** Object:  StoredProcedure [dbo].[ShopifyProductVariantsDelete]    Script Date: 24-06-2020 00:43:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ShopifyProductVariantsDelete]
	@WhereClause nvarchar(1000) = ' Where  Id  <> 0 ',
	@ProductId bigint
AS

	DECLARE @Count int;

	DECLARE @rowCountTable TABLE(row_count INT)

	DECLARE @SQL nvarchar(500)
	
	SET @SQL ='SELECT 0 - COUNT(1) FROM [dbo].[ShopifyProductVariants] '
	SET @SQL = @SQL + @WhereClause
		
	INSERT INTO @rowCountTable 
	Exec(@SQL)
	
	
	SET @SQL ='DELETE FROM [dbo].[ShopifyProductVariants] '
	SET @SQL = @SQL + @WhereClause
		
	EXEC(@SQL)

	SELECT @Count = row_count FROM @rowCountTable

	EXEC ShopifyProductVariantCountUpdate @ProductId, @Count