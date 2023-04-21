CREATE TABLE [dbo].[RequestDetail] (
    [Id]                   INT              IDENTITY (1, 1) NOT NULL,
    [Guid]                 UNIQUEIDENTIFIER CONSTRAINT [DF_RequestDetails_Guid] DEFAULT (newid()) NOT NULL,
    [RequestId]            INT              NOT NULL,
    [SampleName]           VARCHAR (100)    NULL,
    [InitialSampleDate]    SMALLDATETIME    NULL,
    [SampleResultOperator] INT              CONSTRAINT [DF_RequestDetail_SampleResultOperator] DEFAULT ((0)) NOT NULL,
    [InitialSampleResult]  DECIMAL (9, 2)   NULL,
    [FlushSampleDate]      SMALLDATETIME    NULL,
    [FlushResultOperator]  INT              CONSTRAINT [DF_RequestDetail_FlushResultOperator] DEFAULT ((0)) NOT NULL,
    [FlushSampleResult]    DECIMAL (9, 2)   NULL,
    [RemedialActionId]     INT              NULL,
    [MaterialCost]         DECIMAL (8, 2)   NULL,
    [MaterialLabor]        DECIMAL (8, 2)   NULL,
    [CreatedBy]            INT              NOT NULL,
    [CreatedAt]            DATETIME         NOT NULL,
    [UpdatedBy]            INT              NOT NULL,
    [LastUpdated]          DATETIME         CONSTRAINT [DF__RequestDe__LastU__10566F31] DEFAULT (getdate()) NOT NULL,
    [IsActive]             BIT              CONSTRAINT [DF__RequestDe__IsAct__114A936A] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_RequestDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestDetail_RemedialAction] FOREIGN KEY ([RemedialActionId]) REFERENCES [dbo].[RemedialAction] ([Id]),
    CONSTRAINT [FK_RequestDetail_Request] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([Id])
);

