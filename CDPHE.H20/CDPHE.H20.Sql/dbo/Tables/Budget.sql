CREATE TABLE [dbo].[Budget] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [DollarAmount] DECIMAL (12, 2) NULL,
    [CreatedBy]    INT             NOT NULL,
    [CreatedAt]    DATETIME        NOT NULL,
    [UpdatedBy]    INT             NOT NULL,
    [LastUpdated]  DATETIME        DEFAULT (getdate()) NOT NULL,
    [IsActive]     BIT             DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED ([Id] ASC)
);

