CREATE TABLE [dbo].[RequestWiseFile] (
    [RequestWiseFileID] INT           IDENTITY (1, 1) NOT NULL,
    [RequestId]         INT           NOT NULL,
    [FileName]          VARCHAR (500) NOT NULL,
    [CreatedDate]       DATETIME      DEFAULT (getdate()) NOT NULL,
    [PhysicianId]       INT           NULL,
    [AdminId]           INT           NULL,
    [DocType]           SMALLINT      NULL,
    [IsFrontSide]       BIT           NULL,
    [IsCompensation]    BIT           NULL,
    [IP]                VARCHAR (20)  NULL,
    [IsFinalize]        BIT           NULL,
    [IsDeleted]         BIT           NULL,
    [IsPatientRecords]  BIT           NULL,
    PRIMARY KEY CLUSTERED ([RequestWiseFileID] ASC),
    CONSTRAINT [RequestWiseFile_AdminId_fkey] FOREIGN KEY ([AdminId]) REFERENCES [dbo].[Admin] ([AdminId]),
    CONSTRAINT [RequestWiseFile_PhysicianId_fkey] FOREIGN KEY ([PhysicianId]) REFERENCES [dbo].[Physician] ([PhysicianId]),
    CONSTRAINT [RequestWiseFile_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId])
);

