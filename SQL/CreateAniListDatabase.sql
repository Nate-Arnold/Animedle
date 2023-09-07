CREATE TABLE AniListMedia 
(
 ID int PRIMARY KEY IDENTITY(1,1),
 MediaTitle varchar(256) not null,
 MediaType varchar(256) not null,
 Timestamp datetime default(getdate())
)
GO
CREATE PROCEDURE AniList_GetList
AS
SELECT ID, MediaTitle, MediaType
FROM SudokuPuzzles
ORDER BY Timestamp DESC;
GO
CREATE PROCEDURE AniList_SaveMedia
(
	@MediaTitle varchar(256),
	@MediaType varchar(256)
)
AS
INSERT INTO AniListMedia(MediaTitle, MediaType)
VALUES(@MediaTitle, @MediaType)
GO
CREATE PROCEDURE AniList_GetByID
(
	@ID int
)
AS
SELECT ID, MediaTitle, MediaType FROM AniListMedia WHERE ID=@ID
GO
CREATE PROCEDURE AniList_RemoveByID
(
	@ID int
)
AS
DELETE FROM AniListMedia WHERE ID=@ID
GO