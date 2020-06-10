USE [md_db]
GO

/*************************************************************************************************************
Table Name: ShopifyData
Created by:	Mobikasa
Date Created: 5/19/2020

Modification Summary:

*************************************************************************************************************/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShopifyData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JSON] [text] NOT NULL,
	[EventType] [varchar](15) NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[Entity] [varchar](20) NOT NULL,
 CONSTRAINT [PK_ShopifyData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
