CREATE TABLE [dbo].[ShiftDetail] (
    [ShiftDetailId]   INT           IDENTITY (1, 1) NOT NULL,
    [ShiftId]         INT           NOT NULL,
    [ShiftDate]       DATE          NOT NULL,
    [RegionId]        INT           NULL,
    [StartTime]       TIME (7)      NOT NULL,
    [EndTime]         TIME (7)      NOT NULL,
    [Status]          SMALLINT      NOT NULL,
    [ModifiedDate]    DATETIME      NULL,
    [LastRunningDate] DATETIME      NULL,
    [EventId]         VARCHAR (100) NULL,
    [Modifiedby]      INT           NULL,
    [IsDeleted]       BIT           NULL,
    [IsSync]          BIT           NULL,
    PRIMARY KEY CLUSTERED ([ShiftDetailId] ASC),
    CONSTRAINT [ShiftDetail_Modifiedby_fkey] FOREIGN KEY ([Modifiedby]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [ShiftDetail_ShiftId_fkey] FOREIGN KEY ([ShiftId]) REFERENCES [dbo].[Shift] ([ShiftId])
);

