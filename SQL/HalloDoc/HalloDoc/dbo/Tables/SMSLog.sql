CREATE TABLE [dbo].[SMSLog] (
    [SMSLogID]           INT           IDENTITY (1, 1) NOT NULL,
    [SMSTemplate]        VARCHAR (MAX) NOT NULL,
    [MobileNumber]       VARCHAR (50)  NOT NULL,
    [ConfirmationNumber] VARCHAR (200) NULL,
    [RoleId]             INT           NULL,
    [AdminId]            INT           NULL,
    [RequestId]          INT           NULL,
    [PhysicianId]        INT           NULL,
    [CreateDate]         DATE          NOT NULL,
    [SentDate]           DATE          NULL,
    [SentTries]          INT           NOT NULL,
    [Action]             INT           NULL,
    [IsSMSSent]          BIT           NULL,
    PRIMARY KEY CLUSTERED ([SMSLogID] ASC),
    CONSTRAINT [SMSLog_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId]),
    CONSTRAINT [SMSLog_RoleId_fkey] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId])
);

