USE [md_db]
GO

/*************************************************************************************************************
Table Name: ShopifyOrderLineItems
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShopifyOrderLineItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShopifyId] [bigint] NULL,
	[OrderId] [bigint] NOT NULL,
	[VariantId] [bigint] NULL,
	[Title] [varchar](200) NULL,
	[Quantity] [int] NOT NULL,
	[FulfillableQuantity] [int] NULL,
	[SKU] [varchar](100) NOT NULL,
	[Vendor] [varchar](100) NULL,
	[ProductId] [bigint] NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[TaxCode] [varchar](30) NULL,
	[DateAdded] [datetime] NULL,
	[DateUpdated] [datetime] NULL,
	[RegKey] [varchar](255) NULL,
 CONSTRAINT [PK_ShopifyOrderLineItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
