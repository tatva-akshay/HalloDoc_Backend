CREATE TABLE [dbo].[ShiftDetailRegion] (
    [ShiftDetailRegionId] INT IDENTITY (1, 1) NOT NULL,
    [ShiftDetailId]       INT NOT NULL,
    [RegionId]            INT NOT NULL,
    [IsDeleted]           BIT NULL,
    PRIMARY KEY CLUSTERED ([ShiftDetailRegionId] ASC),
    CONSTRAINT [ShiftDetailRegion_RegionId_fkey] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([RegionId]),
    CONSTRAINT [ShiftDetailRegion_ShiftDetailId_fkey] FOREIGN KEY ([ShiftDetailId]) REFERENCES [dbo].[ShiftDetail] ([ShiftDetailId])
);

