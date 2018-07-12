USE [DCC_Kelly]
GO

/****** Object:  Table [dbo].[Talks]    Script Date: 07/10/2018 9:55:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Talks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Summary] [varchar](max) NOT NULL,
	[SpeakerID] [int] NOT NULL,
	[DateGiven] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[DiagnosticInformation] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Talks__3214EC27F109AD56] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[Talks]  WITH CHECK ADD  CONSTRAINT [FK_Talks_Speakers] FOREIGN KEY([SpeakerID])
REFERENCES [dbo].[Speakers] ([ID])
GO

ALTER TABLE [dbo].[Talks] CHECK CONSTRAINT [FK_Talks_Speakers]
GO

