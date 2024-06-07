CREATE TABLE [dbo].[HealthProfessionalType] (
    [HealthProfessionalId] INT          NOT NULL,
    [ProfessionName]       VARCHAR (50) NOT NULL,
    [CreatedDate]          DATETIME     DEFAULT (getdate()) NOT NULL,
    [IsActive]             BIT          NULL,
    [IsDeleted]            BIT          NULL,
    PRIMARY KEY CLUSTERED ([HealthProfessionalId] ASC)
);

