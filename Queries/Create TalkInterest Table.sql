USE [DCC_Kelly]
GO

/****** Object:  Table [dbo].[TalkInterest]    Script Date: 07/10/2018 9:54:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TalkInterest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TalkID] [int] NOT NULL,
	[InterestedRegistrantID] [int] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[DiagnosticInformation] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__TalkInte__3214EC270A829051] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[TalkInterest]  WITH CHECK ADD  CONSTRAINT [FK_TalkInterest_Registrants] FOREIGN KEY([InterestedRegistrantID])
REFERENCES [dbo].[Registrants] ([ID])
GO

ALTER TABLE [dbo].[TalkInterest] CHECK CONSTRAINT [FK_TalkInterest_Registrants]
GO

ALTER TABLE [dbo].[TalkInterest]  WITH CHECK ADD  CONSTRAINT [FK_TalkInterest_Talks] FOREIGN KEY([TalkID])
REFERENCES [dbo].[Talks] ([ID])
GO

ALTER TABLE [dbo].[TalkInterest] CHECK CONSTRAINT [FK_TalkInterest_Talks]
GO

