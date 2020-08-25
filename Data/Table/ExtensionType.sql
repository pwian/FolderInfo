USE [FileStore]
GO

/****** Object:  Table [dbo].[ExtensionType]    Script Date: 8/25/2020 11:46:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ExtensionType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](10) NOT NULL
) ON [PRIMARY]
GO


