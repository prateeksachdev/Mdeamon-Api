USE [md_db]
GO
/****** Object:  StoredProcedure [dbo].[ShopifyOrderInsertUpdate]    Script Date: 23-06-2020 22:51:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ShopifyProductInsertUpdate]
	@ShopifyId bigint, 
	@ShopifyDataId int, 
	@Title nvarchar(255), 
	@Tags nvarchar(255) = null, 
	@ProductType nvarchar(100) = null, 
	@Handle nvarchar(500) = null, 
	@BodyHtml nvarchar(max) = null,
	@ParentShopifyId bigint = null
AS

IF NOT EXISTS( SELECT 1 FROM ShopifyProduct where ShopifyId = @ShopifyId) BEGIN

INSERT INTO [dbo].[ShopifyProduct] (
	[ShopifyId],
	[ShopifyDataId],
	[Title],
	[CreatedOn],
	[UpdatedOn],
	[Tags],
	[ProductType],
	[Handle],
	[BodyHtml],
	[ParentShopifyId]
) VALUES (
	@ShopifyId,
	@ShopifyDataId,
	@Title,
	GETDATE(),
	GETDATE(),
	@Tags,
	@ProductType,
	@Handle,
	@BodyHtml,
	@ParentShopifyId
)


END
ELSE
BEGIN

UPDATE [dbo].[ShopifyProduct] SET
	[ShopifyDataId] = @ShopifyDataId,
	[Title] = @Title,
	[UpdatedOn] = GETDATE(),
	[Tags] = @Tags,
	[ProductType] = @ProductType,
	[Handle] = @Handle,
	[BodyHtml] = @BodyHtml,
	[ParentShopifyId] = @ParentShopifyId
WHERE
	[ShopifyId] = @ShopifyId
	
exec [ShopifyDataUpdate] @ShopifyDataId

END

