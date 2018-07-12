USE [DCC_Kelly]
GO

/****** Object:  Table [dbo].[Log]    Script Date: 07/10/2018 9:09:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetimeoffset](7) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Table] [varchar](50) NULL,
	[Exception] [nvarchar](max) NULL,
	[SourceContext] [nvarchar](max) NULL,
	[MachineName] [varchar](50) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO


