CREATE TABLE [dbo].[UserFacility] (
    [Id]          INT      IDENTITY (1, 1) NOT NULL,
    [UserId]      INT      NOT NULL,
    [FacilityId]  INT      NOT NULL,
    [CreatedBy]   INT      NOT NULL,
    [CreatedAt]   DATETIME NOT NULL,
    [UpdatedBy]   INT      NOT NULL,
    [LastUpdated] DATETIME DEFAULT (getdate()) NOT NULL,
    [IsActive]    BIT      DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_UserFacility] PRIMARY KEY CLUSTERED ([Id] ASC)
);

