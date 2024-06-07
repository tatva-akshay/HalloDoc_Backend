CREATE TABLE [dbo].[WeeklyTimeSheet] (
    [TimeSheetId] INT           IDENTITY (1, 1) NOT NULL,
    [StartDate]   DATE          NOT NULL,
    [EndDate]     DATE          NOT NULL,
    [Status]      INT           NULL,
    [CreatedDate] DATETIME      NULL,
    [ProviderId]  INT           NOT NULL,
    [PayRateId]   INT           NULL,
    [AdminId]     INT           NULL,
    [IsFinalized] BIT           NULL,
    [AdminNote]   VARCHAR (MAX) NULL,
    [BonusAmount] INT           NULL,
    PRIMARY KEY CLUSTERED ([TimeSheetId] ASC),
    CONSTRAINT [WeeklyTimeSheet_AdminId_fkey] FOREIGN KEY ([AdminId]) REFERENCES [dbo].[Admin] ([AdminId]),
    CONSTRAINT [WeeklyTimeSheet_ProviderId_fkey] FOREIGN KEY ([ProviderId]) REFERENCES [dbo].[Physician] ([PhysicianId])
);

