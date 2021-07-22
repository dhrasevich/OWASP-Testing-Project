CREATE TABLE [dbo].[User] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (100) NOT NULL,
    [Email]            NVARCHAR (100) NOT NULL,
    [Password]         NVARCHAR (100) NOT NULL,
    [RegistrationDate] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

