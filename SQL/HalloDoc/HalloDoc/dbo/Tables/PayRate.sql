CREATE TABLE [dbo].[PayRate] (
    [PayRateId]                INT      IDENTITY (1, 1) NOT NULL,
    [PhysicianId]              INT      NOT NULL,
    [NightShiftWeekend]        INT      NULL,
    [Shift]                    INT      NULL,
    [HouseCallNightWeekend]    INT      NULL,
    [PhoneConsult]             INT      NULL,
    [PhoneConsultNightWeekend] INT      NULL,
    [BatchTesting]             INT      NULL,
    [HouseCall]                INT      NULL,
    [CreatedDate]              DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([PayRateId] ASC),
    CONSTRAINT [PayRate_PhysicianId_fkey] FOREIGN KEY ([PhysicianId]) REFERENCES [dbo].[Physician] ([PhysicianId])
);

