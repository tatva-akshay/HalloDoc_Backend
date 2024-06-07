CREATE TABLE [dbo].[WeeklyTimeSheetDetail] (
    [TimeSheetDetailId]        INT           IDENTITY (1, 1) NOT NULL,
    [Date]                     DATE          NOT NULL,
    [NumberOfShifts]           INT           NULL,
    [NightShiftWeekend]        INT           NULL,
    [HouseCall]                INT           NULL,
    [HouseCallNightWeekend]    INT           NULL,
    [PhoneConsult]             INT           NULL,
    [PhoneConsultNightWeekend] INT           NULL,
    [BatchTesting]             INT           NULL,
    [Item]                     VARCHAR (100) NULL,
    [Amount]                   INT           NULL,
    [Bill]                     VARCHAR (100) NULL,
    [TotalAmount]              INT           NULL,
    [BonusAmount]              INT           NULL,
    [TimeSheetId]              INT           NOT NULL,
    [OnCallHours]              INT           NULL,
    [TotalHours]               INT           NULL,
    [IsWeekendHoliday]         BIT           NULL,
    [IsDeleted]                BIT           NULL,
    PRIMARY KEY CLUSTERED ([TimeSheetDetailId] ASC),
    CONSTRAINT [WeeklyTimeSheetDetail_TimeSheetId_fkey] FOREIGN KEY ([TimeSheetId]) REFERENCES [dbo].[WeeklyTimeSheet] ([TimeSheetId])
);

