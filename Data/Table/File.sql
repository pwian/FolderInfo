USE [FileStore]
GO

/****** Object:  Table [dbo].[File]    Script Date: 8/24/2020 8:51:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[File](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](850) NOT NULL,
	[DataCreate] [datetime2](0) NOT NULL,
	[DataModified] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_File2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


