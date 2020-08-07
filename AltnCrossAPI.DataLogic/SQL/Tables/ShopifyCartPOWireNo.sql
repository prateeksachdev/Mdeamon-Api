USE [md_db]
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ShopifyCartPOWireNo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CartToken] [nvarchar](255) NULL,
	[POWireNo] [nvarchar](20) NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK_ShopifyCartPOWireNo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ShopifyCartPOWireNo] ADD  CONSTRAINT [DF_ShopifyCartPOWireNo_DateAdded]  DEFAULT (getdate()) FOR [DateAdded]
GO


