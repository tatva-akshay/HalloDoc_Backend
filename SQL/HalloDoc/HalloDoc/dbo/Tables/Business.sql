CREATE TABLE [dbo].[Business] (
    [BusinessId]   INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (128) NOT NULL,
    [Address1]     VARCHAR (500) NULL,
    [Address2]     VARCHAR (500) NULL,
    [City]         VARCHAR (50)  NULL,
    [RegionId]     INT           NULL,
    [ZipCode]      VARCHAR (100) NULL,
    [PhoneNumber]  VARCHAR (20)  NULL,
    [FaxNumber]    VARCHAR (20)  NULL,
    [CreatedDate]  DATETIME      NULL,
    [ModifiedDate] DATETIME      NULL,
    [Status]       SMALLINT      NULL,
    [IP]           VARCHAR (20)  NULL,
    [CreatedBy]    INT           NULL,
    [ModifiedBy]   INT           NULL,
    [IsDeleted]    BIT           NULL,
    [IsRegistered] BIT           NULL,
    PRIMARY KEY CLUSTERED ([BusinessId] ASC),
    CONSTRAINT [Business_CreatedBy_fkey] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [Business_ModifiedBy_fkey] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [Business_RegionId_fkey] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([RegionId])
);

