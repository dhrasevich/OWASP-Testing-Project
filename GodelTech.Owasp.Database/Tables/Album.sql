CREATE TABLE [dbo].[Album]
(
	[AlbumId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GenreId] INT NULL FOREIGN KEY REFERENCES Genre(GenreId), 
    [ArtistId] INT NULL FOREIGN KEY REFERENCES Artist(ArtistId), 
    [Title] NVARCHAR(160) NULL, 
    [Price] DECIMAL(18, 2) NULL, 
    [AlbumArtUrl] NVARCHAR(1024) NULL
)
