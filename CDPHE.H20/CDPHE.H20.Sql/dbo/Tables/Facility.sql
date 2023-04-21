CREATE TABLE [dbo].[Facility] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [Guid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Facilities_Guid] DEFAULT (newid()) NOT NULL,
    [WQCID]       VARCHAR (50)     NULL,
    [OrgId]       INT              NOT NULL,
    [Name]        VARCHAR (100)    NULL,
    [Address1]    VARCHAR (100)    NULL,
    [Address2]    VARCHAR (50)     NULL,
    [City]        VARCHAR (50)     NULL,
    [State]       VARCHAR (2)      NULL,
    [ZipCode]     VARCHAR (10)     NULL,
    [CreatedBy]   INT              NOT NULL,
    [CreatedAt]   DATETIME         NOT NULL,
    [UpdatedBy]   INT              NOT NULL,
    [LastUpdated] DATETIME         CONSTRAINT [DF__Facilitie__LastU__7E37BEF6] DEFAULT (getdate()) NOT NULL,
    [IsActive]    BIT              CONSTRAINT [DF__Facilitie__IsAct__7F2BE32F] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Facilities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Facility_Organization] FOREIGN KEY ([OrgId]) REFERENCES [dbo].[Organization] ([Id])
);

