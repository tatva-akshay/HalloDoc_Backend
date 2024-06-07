CREATE TABLE [dbo].[BlockRequests] (
    [BlockRequestId] INT           IDENTITY (1, 1) NOT NULL,
    [PhoneNumber]    VARCHAR (MAX) NULL,
    [Email]          VARCHAR (MAX) NULL,
    [Reason]         VARCHAR (MAX) NULL,
    [IP]             VARCHAR (MAX) NULL,
    [CreatedDate]    DATE          NULL,
    [ModifiedDate]   DATE          NULL,
    [RequestId]      INT           NOT NULL,
    [IsActive]       BIT           NULL,
    PRIMARY KEY CLUSTERED ([BlockRequestId] ASC),
    CONSTRAINT [BlockRequests_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId])
);

