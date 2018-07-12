USE [DCC_Kelly]
GO

/****** Object:  Table [dbo].[Registrants]    Script Date: 07/10/2018 9:47:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Registrants](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailAddress] [nvarchar](50) NOT NULL,
	[Occupation] [varchar](50) NULL,
	[BirthDate] [datetime] NULL,
	[UpdateTime] [datetime] NOT NULL,
	[DiagnosticInformation] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Registra__3214EC2721F93DE4] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

