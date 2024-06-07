CREATE TABLE [dbo].[OrderDetails] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [VendorId]        INT           NULL,
    [RequestId]       INT           NULL,
    [FaxNumber]       VARCHAR (50)  NULL,
    [Email]           VARCHAR (50)  NULL,
    [BusinessContact] VARCHAR (100) NULL,
    [Prescription]    VARCHAR (MAX) NULL,
    [NoOfRefill]      INT           NULL,
    [CreatedDate]     DATETIME      NULL,
    [CreatedBy]       VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [OrderDetails_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId])
);

