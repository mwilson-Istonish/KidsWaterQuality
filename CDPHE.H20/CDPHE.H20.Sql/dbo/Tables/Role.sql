CREATE TABLE [dbo].[Role] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [CreatedBy]   INT          NOT NULL,
    [CreatedAt]   DATETIME     NOT NULL,
    [UpdatedBy]   INT          NOT NULL,
    [LastUpdated] DATETIME     DEFAULT (getdate()) NOT NULL,
    [IsActive]    BIT          DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);

