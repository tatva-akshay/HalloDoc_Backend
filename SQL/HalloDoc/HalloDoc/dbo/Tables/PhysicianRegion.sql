CREATE TABLE [dbo].[PhysicianRegion] (
    [PhysicianRegionId] INT IDENTITY (1, 1) NOT NULL,
    [PhysicianId]       INT NOT NULL,
    [RegionId]          INT NOT NULL,
    PRIMARY KEY CLUSTERED ([PhysicianRegionId] ASC),
    CONSTRAINT [PhysicianRegion_PhysicianId_fkey] FOREIGN KEY ([PhysicianId]) REFERENCES [dbo].[Physician] ([PhysicianId]),
    CONSTRAINT [PhysicianRegion_RegionId_fkey] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([RegionId])
);

