CREATE TABLE [dbo].[Concierge] (
    [ConciergeId]   INT           IDENTITY (1, 1) NOT NULL,
    [ConciergeName] VARCHAR (100) NOT NULL,
    [Address]       VARCHAR (150) NULL,
    [Street]        VARCHAR (50)  NOT NULL,
    [City]          VARCHAR (50)  NOT NULL,
    [State]         VARCHAR (50)  NOT NULL,
    [ZipCode]       VARCHAR (50)  NOT NULL,
    [CreatedDate]   DATETIME      DEFAULT (getdate()) NOT NULL,
    [RegionId]      INT           NULL,
    [RoleId]        VARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([ConciergeId] ASC),
    CONSTRAINT [Concierge_RegionId_fkey] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([RegionId])
);

