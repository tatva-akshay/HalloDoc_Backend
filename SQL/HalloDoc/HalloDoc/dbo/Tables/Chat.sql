CREATE TABLE [dbo].[Chat] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [AdminId]     INT           NULL,
    [ProviderId]  INT           NULL,
    [RequestId]   INT           NULL,
    [Message]     VARCHAR (MAX) NULL,
    [CreatedDate] DATETIME      NULL,
    [SentBy]      INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Chat_AdminId_fkey] FOREIGN KEY ([AdminId]) REFERENCES [dbo].[Admin] ([AdminId]),
    CONSTRAINT [Chat_ProviderId_fkey] FOREIGN KEY ([ProviderId]) REFERENCES [dbo].[Physician] ([PhysicianId]),
    CONSTRAINT [Chat_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId])
);

