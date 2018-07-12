CREATE TABLE [dbo].[Talks] 
(ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 Title varchar(100) NOT NULL,
 Summary varchar(MAX) NOT NULL,
 PresenterRegistrantID int NOT NULL);

CREATE TABLE [dbo].[Registrants]
(ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 FirstName varchar(50) NOT NULL,
 LastName varchar(50) NOT NULL);

CREATE TABLE [dbo].[TalkInterest]
(ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 TalkID INT NOT NULL,
 InterestedRegistrantID INT NOT NULL);

CREATE TABLE [dbo].[Rooms]
(ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 RoomName varchar(50) NOT NULL,
 Capacity int NOT NULL,
 Venue varchar(255));

CREATE TABLE [dbo].[Sessions]
(ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 SessionDate date NOT NULL,
 TimeStart time(7) NOT NULL,
 TimeEnd time(7) NOT NULL);



--ALTER TABLE [dbo].[Registrants] DROP COLUMN ID;
--ALTER TABLE [dbo].[Registrants] ADD ID INT IDENTITY(1,1);

--ALTER TABLE [dbo].[Rooms] DROP COLUMN ID;
--ALTER TABLE [dbo].[Rooms] ADD ID INT IDENTITY(1,1);

--ALTER TABLE [dbo].[Sessions] DROP COLUMN ID;
--ALTER TABLE [dbo].[Sessions] ADD ID INT IDENTITY(1,1);

--ALTER TABLE [dbo].[TalkInterest] DROP COLUMN ID;
--ALTER TABLE [dbo].[TalkInterest] ADD ID INT IDENTITY(1,1);

--ALTER TABLE [dbo].[Talks] DROP COLUMN ID;
--ALTER TABLE [dbo].[Talks] ADD ID INT IDENTITY(1,1);


--ALTER TABLE [dbo].[Registrants] NOCHECK CONSTRAINT DF_Registrants_ID
--ALTER TABLE [dbo].[Rooms] NOCHECK CONSTRAINT ALL
--ALTER TABLE [dbo].[Sessions] NOCHECK CONSTRAINT ALL
--ALTER TABLE [dbo].[TalkInterest] NOCHECK CONSTRAINT ALL
--ALTER TABLE [dbo].[Talks] NOCHECK CONSTRAINT ALL