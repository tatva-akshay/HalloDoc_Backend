CREATE TABLE [dbo].[PhysicianLocation] (
    [LocationId]    INT              IDENTITY (2, 1) NOT NULL,
    [PhysicianId]   INT              NOT NULL,
    [Latitude]      NUMERIC (18, 10) NULL,
    [Longitude]     NUMERIC (18, 10) NULL,
    [CreatedDate]   DATETIME         NOT NULL,
    [PhysicianName] VARCHAR (50)     NULL,
    [Address]       VARCHAR (500)    NULL,
    PRIMARY KEY CLUSTERED ([LocationId] ASC),
    CONSTRAINT [PhysicianLocation_PhysicianId_fkey] FOREIGN KEY ([PhysicianId]) REFERENCES [dbo].[Physician] ([PhysicianId])
);

