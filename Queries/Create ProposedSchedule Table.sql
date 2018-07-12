USE [DCC_Kelly]
GO

/****** Object:  Table [dbo].[ProposedSchedule]    Script Date: 07/10/2018 9:13:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProposedSchedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TalkID] [int] NOT NULL,
	[SessionID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[DiagnosticInformation] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ProposedSchedule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[ProposedSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ProposedSchedule_Rooms] FOREIGN KEY([RoomID])
REFERENCES [dbo].[Rooms] ([ID])
GO

ALTER TABLE [dbo].[ProposedSchedule] CHECK CONSTRAINT [FK_ProposedSchedule_Rooms]
GO

ALTER TABLE [dbo].[ProposedSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ProposedSchedule_Sessions] FOREIGN KEY([SessionID])
REFERENCES [dbo].[Sessions] ([ID])
GO

ALTER TABLE [dbo].[ProposedSchedule] CHECK CONSTRAINT [FK_ProposedSchedule_Sessions]
GO

ALTER TABLE [dbo].[ProposedSchedule]  WITH CHECK ADD  CONSTRAINT [FK_ProposedSchedule_Talks] FOREIGN KEY([TalkID])
REFERENCES [dbo].[Talks] ([ID])
GO

ALTER TABLE [dbo].[ProposedSchedule] CHECK CONSTRAINT [FK_ProposedSchedule_Talks]
GO

