USE [md_db]
GO

CREATE TABLE [dbo].[ShopifyProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShopifyId] [bigint] NULL,
	[ShopifyDataId] [int] NULL,
	[Title] [nvarchar](255) NULL,
	[Tags] [nvarchar](255) NULL,
	[ProductType] [nvarchar](100) NULL,
	[Handle] [nvarchar](500) NULL,
	[BodyHtml] [nvarchar](max) NULL,
	[VariantsCount] [int] NULL,
	[Meta] [nvarchar](500) NULL,
	[ParentShopifyId] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_ShopifyProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ShopifyProduct] UNIQUE NONCLUSTERED 
(
	[ShopifyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


