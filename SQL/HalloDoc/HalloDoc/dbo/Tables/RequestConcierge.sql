CREATE TABLE [dbo].[RequestConcierge] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [RequestId]   INT          NOT NULL,
    [ConciergeId] INT          NOT NULL,
    [IP]          VARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [RequestConcierge_ConciergeId_fkey] FOREIGN KEY ([ConciergeId]) REFERENCES [dbo].[Concierge] ([ConciergeId]),
    CONSTRAINT [RequestConcierge_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId])
);

