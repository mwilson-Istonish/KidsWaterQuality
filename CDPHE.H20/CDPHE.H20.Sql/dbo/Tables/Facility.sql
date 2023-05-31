CREATE TABLE [dbo].[Facility] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [Guid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Facilities_Guid] DEFAULT (newid()) NOT NULL,
    [WQCID]       VARCHAR (50)     NOT NULL,
    [Name]        VARCHAR (100)    NOT NULL,
    [Address1]    VARCHAR (100)    NOT NULL,
    [Address2]    VARCHAR (50)     NULL,
    [Address3]    VARCHAR (50)     NULL,
    [City]        VARCHAR (50)     NOT NULL,
    [County]      VARCHAR (50)     NOT NULL,
    [State]       VARCHAR (2)      NOT NULL,
    [ZipCode]     VARCHAR (10)     NOT NULL,
    [CreatedBy]   INT              NOT NULL,
    [CreatedAt]   DATETIME         NOT NULL,
    [UpdatedBy]   INT              NOT NULL,
    [LastUpdated] DATETIME         CONSTRAINT [DF__Facilitie__LastU__7E37BEF6] DEFAULT (getdate()) NOT NULL,
    [IsActive]    BIT              CONSTRAINT [DF__Facilitie__IsAct__7F2BE32F] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Facilities] PRIMARY KEY CLUSTERED ([Id] ASC)
);



