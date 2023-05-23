CREATE TABLE [dbo].[TermsOfService] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Body]        VARCHAR (MAX) NULL,
    [CreatedBy]   INT           NOT NULL,
    [CreatedAt]   DATETIME      NOT NULL,
    [UpdatedBy]   INT           NOT NULL,
    [LastUpdated] DATETIME      CONSTRAINT [DF__TermsOfSe__LastU__0F624AF8] DEFAULT (getdate()) NOT NULL,
    [IsActive]    BIT           CONSTRAINT [DF__TermsOfSe__IsAct__10566F31] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TermsOfService] PRIMARY KEY CLUSTERED ([Id] ASC)
);

