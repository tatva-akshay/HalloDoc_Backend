CREATE TABLE [dbo].[RequestBusiness] (
    [RequestBusinessId] INT          IDENTITY (1, 1) NOT NULL,
    [RequestId]         INT          NOT NULL,
    [BusinessId]        INT          NOT NULL,
    [IP]                VARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([RequestBusinessId] ASC),
    CONSTRAINT [RequestBusiness_BusinessId_fkey] FOREIGN KEY ([BusinessId]) REFERENCES [dbo].[Business] ([BusinessId]),
    CONSTRAINT [RequestBusiness_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId])
);

