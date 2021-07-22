CREATE TABLE [dbo].[Album] (
    [AlbumId]     INT             IDENTITY (1, 1) NOT NULL,
    [GenreId]     INT             NULL,
    [ArtistId]    INT             NULL,
    [Title]       NVARCHAR (160)  NULL,
    [Price]       DECIMAL (18, 2) NULL,
    [AlbumArtUrl] NVARCHAR (1024) NULL,
    PRIMARY KEY CLUSTERED ([AlbumId] ASC),
    FOREIGN KEY ([ArtistId]) REFERENCES [dbo].[Artist] ([ArtistId]),
    FOREIGN KEY ([GenreId]) REFERENCES [dbo].[Genre] ([GenreId])
);

