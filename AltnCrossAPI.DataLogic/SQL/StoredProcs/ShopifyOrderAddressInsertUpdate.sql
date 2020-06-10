USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyOrderAddressInsertUpdate
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[ShopifyOrderAddressInsertUpdate]
	@FirstName varchar(100),
	@LastName varchar(100),
	@Address varchar(200),
	@Phone varchar(50),
	@City varchar(20),
	@Zip varchar(10),
	@Province varchar(50),
	@Country varchar(30),
	@Latitude decimal(12, 9),
	@Longitude decimal(12, 9),
	@CountryCode varchar(5),
	@OrderId bigint,
	@AddressType smallint
AS

IF NOT EXISTS( SELECT 1 FROM ShopifyOrderAddress where OrderId = @OrderId and AddressType = @AddressType) BEGIN

INSERT INTO [dbo].[ShopifyOrderAddress] (
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
) VALUES (
	@FirstName,
	@LastName,
	@Address,
	@Phone,
	@City,
	@Zip,
	@Province,
	@Country,
	@Latitude,
	@Longitude,
	@CountryCode,
	GETDATE(),
	GETDATE(),
	@OrderId,
	@AddressType
)

END
ELSE
BEGIN

UPDATE [dbo].[ShopifyOrderAddress] SET
	[FirstName] = @FirstName,
	[LastName] = @LastName,
	[Address] = @Address,
	[Phone] = @Phone,
	[City] = @City,
	[Zip] = @Zip,
	[Province] = @Province,
	[Country] = @Country,
	[Latitude] = @Latitude,
	[Longitude] = @Longitude,
	[CountryCode] = @CountryCode,
	[DateUpdated] = GETDATE()
WHERE
	[OrderId] = @OrderId and [AddressType] = @AddressType
	

END

GO
