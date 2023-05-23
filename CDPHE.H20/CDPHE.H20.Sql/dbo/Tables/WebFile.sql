CREATE TABLE [dbo].[WebFile] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [RequestId]   INT           NOT NULL,
    [Name]        VARCHAR (50)  NULL,
    [Url]         VARCHAR (250) NOT NULL,
    [FileType]    VARCHAR (25)  NOT NULL,
    [CreatedBy]   INT           NOT NULL,
    [CreatedAt]   DATETIME      NOT NULL,
    [UpdatedBy]   INT           NOT NULL,
    [LastUpdated] DATETIME      DEFAULT (getdate()) NOT NULL,
    [IsActive]    BIT           DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_WebFile] PRIMARY KEY CLUSTERED ([Id] ASC)
);

