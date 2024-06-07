CREATE TABLE [dbo].[EmailLog] (
    [EmailLogID]         INT           IDENTITY (2, 1) NOT NULL,
    [EmailTemplate]      VARCHAR (MAX) NOT NULL,
    [SubjectName]        VARCHAR (200) NOT NULL,
    [EmailID]            VARCHAR (200) NOT NULL,
    [ConfirmationNumber] VARCHAR (200) NULL,
    [FilePath]           VARCHAR (MAX) NULL,
    [RoleId]             INT           NULL,
    [RequestId]          INT           NULL,
    [AdminId]            INT           NULL,
    [PhysicianId]        INT           NULL,
    [CreateDate]         DATE          NOT NULL,
    [SentDate]           DATE          NULL,
    [SentTries]          INT           NULL,
    [Action]             INT           NULL,
    [IsEmailSent]        BIT           NULL,
    PRIMARY KEY CLUSTERED ([EmailLogID] ASC),
    CONSTRAINT [EmailLog_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId]),
    CONSTRAINT [EmailLog_RoleId_fkey] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId])
);

