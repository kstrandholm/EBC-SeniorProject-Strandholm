USE [DCC_Kelly]
GO

/****** Object:  Table [dbo].[Speakers]    Script Date: 07/10/2018 9:50:27 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Speakers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[DiagnosticInformation] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Speakers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO