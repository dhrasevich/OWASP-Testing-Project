CREATE TABLE [dbo].[Genre] (
    [GenreId]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([GenreId] ASC)
);

