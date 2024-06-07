CREATE TABLE [dbo].[RequestType] (
    [RequestTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([RequestTypeId] ASC)
);

