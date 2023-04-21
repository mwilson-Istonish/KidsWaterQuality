CREATE TABLE [dbo].[Organization] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (150) NOT NULL,
    [W9Url]       VARCHAR (500) NULL,
    [CreatedBy]   INT           NOT NULL,
    [CreatedAt]   DATETIME      NOT NULL,
    [UpdatedBy]   INT           NOT NULL,
    [LastUpdated] DATETIME      CONSTRAINT [DF__Organizat__LastU__5070F446] DEFAULT (getdate()) NOT NULL,
    [IsActive]    BIT           CONSTRAINT [DF__Organizat__IsAct__5165187F] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED ([Id] ASC)
);

