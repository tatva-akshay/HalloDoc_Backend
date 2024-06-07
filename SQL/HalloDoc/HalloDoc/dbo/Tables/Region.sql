CREATE TABLE [dbo].[Region] (
    [RegionId]     INT           NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [Abbreviation] NVARCHAR (50) NULL,
    CONSTRAINT [Region_pkey] PRIMARY KEY CLUSTERED ([RegionId] ASC)
);

