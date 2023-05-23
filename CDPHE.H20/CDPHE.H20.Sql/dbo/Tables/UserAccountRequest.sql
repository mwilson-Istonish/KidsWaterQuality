CREATE TABLE [dbo].[UserAccountRequest] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]   VARCHAR (50)  NOT NULL,
    [LastName]    VARCHAR (50)  NOT NULL,
    [Email]       VARCHAR (250) NOT NULL,
    [RequestDate] DATETIME      NOT NULL,
    [IpAddress]   VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

