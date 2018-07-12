
INSERT INTO [dbo].[Sessions] (SessionDate, TimeStart, TimeEnd)
VALUES (CONVERT(date, GETDATE()), CONVERT(smalldatetime, GETDATE()), '2018-06-11 22:30')

SELECT * FROM [dbo].[Sessions]


--DELETE FROM [dbo].[Sessions]


INSERT INTO [dbo].[Rooms] (RoomName, Capacity)
VALUES ('Royale I', 20);
INSERT INTO [dbo].[Rooms] (RoomName, Capacity)
VALUES ('Royale II', 30);
INSERT INTO [dbo].[Rooms] (RoomName, Capacity)
VALUES ('Grande I', 40);
INSERT INTO [dbo].[Rooms] (RoomName, Capacity)
VALUES ('Grande II', 40);