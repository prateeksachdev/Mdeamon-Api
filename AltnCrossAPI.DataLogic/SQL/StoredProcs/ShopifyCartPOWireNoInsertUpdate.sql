USE [md_db]
GO


CREATE PROCEDURE [dbo].[ShopifyCartPOWireNoInsertUpdate]
	@CartToken nvarchar(255), 
	@POWireNo nvarchar(20)
AS

IF NOT EXISTS( SELECT 1 FROM ShopifyCartPOWireNo where [CartToken] = @CartToken) BEGIN

INSERT INTO [dbo].[ShopifyCartPOWireNo] (
	[CartToken],
	[POWireNo]
) VALUES (
	@CartToken,
	@POWireNo
)


END
ELSE
BEGIN

UPDATE [dbo].[ShopifyCartPOWireNo] SET
	[POWireNo] = @POWireNo
WHERE
	[CartToken] = @CartToken
	
END


