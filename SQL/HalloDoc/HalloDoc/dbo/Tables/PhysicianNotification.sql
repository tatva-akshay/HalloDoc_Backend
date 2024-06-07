CREATE TABLE [dbo].[PhysicianNotification] (
    [Id]                    INT IDENTITY (4, 1) NOT NULL,
    [PhysicianId]           INT NOT NULL,
    [IsNotificationStopped] BIT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [PhysicianNotification_PhysicianId_fkey] FOREIGN KEY ([PhysicianId]) REFERENCES [dbo].[Physician] ([PhysicianId])
);

