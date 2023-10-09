CREATE TABLE AniListMedia 
(
 ID int,
 TitleRomaji varchar(256),
 TitleEnglish varchar(256),
 TitleNative nvarchar(256),
 MediaData varchar(max),
 Timestamp datetime default(getdate())
)
GO
CREATE PROCEDURE AniListGetTitles
AS
SELECT ID, TitleRomaji, TitleEnglish, TitleNative
FROM AniListMedia
ORDER BY Timestamp DESC;
GO
CREATE PROCEDURE AniListSaveMedia
(
	@ID int,
	@TitleRomaji varchar(256),
	@TitleEnglish varchar(256),
	@TitleNative nvarchar(256),
	@MediaData varchar(max)
)
AS
INSERT INTO AniListMedia(ID, TitleRomaji, TitleEnglish, TitleNative, MediaData)
VALUES(@ID, @TitleRomaji, @TitleEnglish, @TitleNative, @MediaData)
GO
CREATE PROCEDURE AniListGetByRomaji
(
	@TitleRomaji varchar(256)
)
AS
SELECT MediaData FROM AniListMedia WHERE TitleRomaji=@TitleRomaji
GO
CREATE PROCEDURE AniListGetByEnglish
(
	@TitleEnglish varchar(256)
)
AS
SELECT MediaData FROM AniListMedia WHERE TitleEnglish=@TitleEnglish
GO
CREATE PROCEDURE AniListGetByNative
(
	@TitleNative nvarchar(256)
)
AS
SELECT MediaData FROM AniListMedia WHERE TitleNative=@TitleNative
GO
CREATE PROCEDURE AniListRemoveByID
(
	@ID int
)
AS
DELETE FROM AniListMedia WHERE ID=@ID
GO
CREATE PROCEDURE AniListClearDatabase
AS
DELETE FROM AniListMedia
GO