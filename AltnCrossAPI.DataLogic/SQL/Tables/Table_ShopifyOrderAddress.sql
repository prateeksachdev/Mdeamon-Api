USE [md_db]
GO

/*************************************************************************************************************
Table Name: ShopifyOrderAddress
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShopifyOrderAddress](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Address] [varchar](200) NULL,
	[Phone] [varchar](50) NULL,
	[City] [varchar](20) NULL,
	[Zip] [varchar](10) NULL,
	[Province] [varchar](50) NULL,
	[Country] [varchar](30) NULL,
	[Latitude] [decimal](12, 9) NULL,
	[Longitude] [decimal](12, 9) NULL,
	[CountryCode] [varchar](5) NULL,
	[DateAdded] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[OrderId] [bigint] NOT NULL,
	[AddressType] [smallint] NOT NULL,
 CONSTRAINT [PK_ShopifyOrderAddress] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
