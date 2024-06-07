CREATE TABLE [dbo].[Shift] (
    [ShiftId]     INT          IDENTITY (1, 1) NOT NULL,
    [PhysicianId] INT          NOT NULL,
    [StartDate]   DATE         NOT NULL,
    [WeekDays]    CHAR (7)     NULL,
    [RepeatUpto]  INT          NULL,
    [CreatedDate] DATETIME     NOT NULL,
    [IP]          VARCHAR (20) NULL,
    [CreatedBy]   INT          NOT NULL,
    [IsRepeat]    BIT          NULL,
    PRIMARY KEY CLUSTERED ([ShiftId] ASC),
    CONSTRAINT [Shift_CreatedBy_fkey] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [Shift_PhysicianId_fkey] FOREIGN KEY ([PhysicianId]) REFERENCES [dbo].[Physician] ([PhysicianId])
);

