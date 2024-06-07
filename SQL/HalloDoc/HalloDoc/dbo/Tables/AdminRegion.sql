CREATE TABLE [dbo].[AdminRegion] (
    [AdminRegionId] INT IDENTITY (1, 1) NOT NULL,
    [AdminId]       INT NOT NULL,
    [RegionId]      INT NOT NULL,
    PRIMARY KEY CLUSTERED ([AdminRegionId] ASC),
    CONSTRAINT [AdminRegion_AdminId_fkey] FOREIGN KEY ([AdminId]) REFERENCES [dbo].[Admin] ([AdminId]),
    CONSTRAINT [AdminRegion_RegionId_fkey] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([RegionId])
);

