CREATE TABLE [dbo].[RequestStatusLog] (
    [RequestStatusLogId] INT           IDENTITY (1, 1) NOT NULL,
    [RequestId]          INT           NOT NULL,
    [Status]             SMALLINT      NOT NULL,
    [PhysicianId]        INT           NULL,
    [AdminId]            INT           NULL,
    [TransToPhysicianId] INT           NULL,
    [Notes]              VARCHAR (500) NULL,
    [CreatedDate]        DATETIME      DEFAULT (getdate()) NOT NULL,
    [IP]                 VARCHAR (20)  NULL,
    [TransToAdmin]       BIT           NULL,
    PRIMARY KEY CLUSTERED ([RequestStatusLogId] ASC),
    CONSTRAINT [RequestStatusLog_AdminId_fkey] FOREIGN KEY ([AdminId]) REFERENCES [dbo].[Admin] ([AdminId]),
    CONSTRAINT [RequestStatusLog_PhysicianId_fkey] FOREIGN KEY ([PhysicianId]) REFERENCES [dbo].[Physician] ([PhysicianId]),
    CONSTRAINT [RequestStatusLog_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId]),
    CONSTRAINT [RequestStatusLog_TransToPhysicianId_fkey] FOREIGN KEY ([TransToPhysicianId]) REFERENCES [dbo].[Physician] ([PhysicianId])
);

