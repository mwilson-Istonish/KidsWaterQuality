CREATE TABLE [dbo].[Request] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [UserId]      INT          NOT NULL,
    [FacilityId]  INT          NOT NULL,
    [Status]      VARCHAR (50) NULL,
    [CreatedBy]   INT          NOT NULL,
    [CreatedAt]   DATETIME     NOT NULL,
    [UpdatedBy]   INT          NOT NULL,
    [LastUpdated] DATETIME     CONSTRAINT [DF__Requests__LastUp__74AE54BC] DEFAULT (getdate()) NOT NULL,
    [IsActive]    BIT          CONSTRAINT [DF__Requests__IsActi__75A278F5] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Request_Facility] FOREIGN KEY ([FacilityId]) REFERENCES [dbo].[Facility] ([Id])
);



