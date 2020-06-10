USE [md_db]
GO

/*************************************************************************************************************
Procedure Name: ShopifyDataInsert
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

CREATE PROCEDURE [dbo].[ShopifyDataInsert]
	@JSON text,
	@EventType varchar(15),
	@DateAdded datetime,
	@Entity varchar(20),
	@Id int OUTPUT
AS

INSERT INTO [dbo].[ShopifyData] (
	[JSON],
	[EventType],
	[DateAdded],
	[Entity]
) VALUES (
	@JSON,
	@EventType,
	@DateAdded,
	@Entity
)

SET @Id = @@IDENTITY

GO
