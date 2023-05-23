CREATE TABLE [dbo].[RemedialAction] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (100)   NULL,
    [CreatedBy]   INT             NOT NULL,
    [CreatedAt]   DATETIME        NOT NULL,
    [UpdatedBy]   INT             NOT NULL,
    [LastUpdated] DATETIME        DEFAULT (getdate()) NOT NULL,
    [IsActive]    BIT             DEFAULT ((1)) NOT NULL,
    [NotToExceed] DECIMAL (10, 2) NULL,
    CONSTRAINT [PK_RemedialActions] PRIMARY KEY CLUSTERED ([Id] ASC)
);



