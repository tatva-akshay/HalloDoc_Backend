CREATE TABLE [dbo].[HealthProfessionals] (
    [VendorId]        INT           IDENTITY (4, 1) NOT NULL,
    [VendorName]      VARCHAR (100) NOT NULL,
    [Profession]      INT           NULL,
    [FaxNumber]       VARCHAR (50)  NOT NULL,
    [Address]         VARCHAR (150) NULL,
    [City]            VARCHAR (100) NULL,
    [State]           VARCHAR (50)  NULL,
    [Zip]             VARCHAR (50)  NULL,
    [RegionId]        INT           NULL,
    [CreatedDate]     DATETIME      DEFAULT (getdate()) NOT NULL,
    [ModifiedDate]    DATETIME      NULL,
    [PhoneNumber]     VARCHAR (100) NULL,
    [IP]              VARCHAR (20)  NULL,
    [Email]           VARCHAR (50)  NULL,
    [BusinessContact] VARCHAR (100) NULL,
    [IsDeleted]       BIT           NULL,
    PRIMARY KEY CLUSTERED ([VendorId] ASC),
    CONSTRAINT [HealthProfessionals_Profession_fkey] FOREIGN KEY ([Profession]) REFERENCES [dbo].[HealthProfessionalType] ([HealthProfessionalId])
);

