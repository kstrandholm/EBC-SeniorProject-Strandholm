USE [DCC_Kelly]
GO

/****** Object:  Table [dbo].[Sessions]    Script Date: 07/10/2018 9:49:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sessions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SessionDate] [datetime] NOT NULL,
	[TimeStart] [datetime] NOT NULL,
	[TimeEnd] [datetime] NOT NULL,
	[CalcLengthMinutes]  AS (datediff(minute,[TimeStart],[TimeEnd])) PERSISTED,
	[UpdateTime] [datetime] NOT NULL,
	[DiagnosticInformation] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Sessions__3214EC2799F838A9] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO