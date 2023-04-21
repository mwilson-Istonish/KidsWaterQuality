CREATE TABLE [dbo].[User] (
    [id]                 INT              IDENTITY (1, 1) NOT NULL,
    [Guid]               UNIQUEIDENTIFIER CONSTRAINT [DF_Users_Guid] DEFAULT (newid()) NOT NULL,
    [RoleId]             INT              NULL,
    [FirstName]          VARCHAR (50)     NOT NULL,
    [LastName]           VARCHAR (50)     NOT NULL,
    [Email]              VARCHAR (250)    NOT NULL,
    [LoginKey]           VARCHAR (50)     NULL,
    [LoginKeyExpiration] DATETIME         NULL,
    [LastLoggedIn]       DATETIME         NULL,
    [CreatedBy]          INT              NOT NULL,
    [CreatedAt]          DATETIME         NOT NULL,
    [UpdatedBy]          INT              NOT NULL,
    [LastUpdated]        DATETIME         CONSTRAINT [DF__Users__LastUpdat__5CD6CB2B] DEFAULT (getdate()) NOT NULL,
    [IsActive]           BIT              CONSTRAINT [DF__Users__IsActive__5DCAEF64] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
);

