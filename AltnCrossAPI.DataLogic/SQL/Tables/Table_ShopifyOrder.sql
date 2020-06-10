USE [md_db]
GO

/*************************************************************************************************************
Table Name: ShopifyOrder
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShopifyOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShopifyId] [bigint] NOT NULL,
	[ShopifyDataId] [int] NULL,
	[OrderNumber] [int] NULL,
	[Email] [varchar](100) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NOT NULL,
	[ProcessedOn] [datetime] NULL,
	[Token] [nvarchar](100) NULL,
	[CheckoutToken] [nvarchar](100) NULL,
	[Gateway] [nvarchar](100) NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[TotalDiscount] [decimal](18, 2) NULL,
	[SubTotalPrice] [decimal](18, 2) NOT NULL,
	[TotalTax] [decimal](18, 2) NULL,
	[FinancialStatus] [varchar](10) NULL,
	[ProcessingMethod] [varchar](25) NULL,
	[Currency] [varchar](5) NULL,
	[CheckoutId] [bigint] NULL,
	[AppId] [bigint] NULL,
	[BrowserIP] [varchar](30) NULL,
	[OrderStatusUrl] [varchar](500) NULL,
 CONSTRAINT [PK_ShopifyOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
