CREATE TABLE [dbo].[RequestClosed] (
    [RequestClosedId]    INT           IDENTITY (1, 1) NOT NULL,
    [RequestId]          INT           NOT NULL,
    [RequestStatusLogId] INT           NOT NULL,
    [PhyNotes]           VARCHAR (500) NULL,
    [ClientNotes]        VARCHAR (500) NULL,
    [IP]                 VARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([RequestClosedId] ASC),
    CONSTRAINT [RequestClosed_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId])
);

