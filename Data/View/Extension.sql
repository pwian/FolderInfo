USE [FileStore]
GO

/****** Object:  View [dbo].[Extension]    Script Date: 8/24/2020 9:32:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Extension]
AS
SELECT DISTINCT TOP (100) PERCENT RIGHT(FileName, CHARINDEX('.', REVERSE(FileName))) AS ExtensionName
FROM            dbo.[File]
WHERE        (CHARINDEX('.', REVERSE(FileName)) < CHARINDEX('\', REVERSE(FileName)))
ORDER BY ExtensionName
GO


