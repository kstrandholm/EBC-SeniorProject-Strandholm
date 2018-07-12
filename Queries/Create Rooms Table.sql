USE [DCC_Kelly]
GO

/****** Object:  Table [dbo].[Rooms]    Script Date: 07/10/2018 9:58:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rooms](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [varchar](50) NOT NULL,
	[Capacity] [int] NOT NULL,
	[Venue] [varchar](255) NULL,
	[UpdateTime] [datetime] NOT NULL,
	[DiagnosticInformation] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Rooms__3214EC272A6B4AC1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

