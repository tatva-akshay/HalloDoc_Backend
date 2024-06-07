CREATE TABLE [dbo].[Menu] (
    [MenuId]      INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [AccountType] SMALLINT     NOT NULL,
    [SortOrder]   INT          NULL,
    PRIMARY KEY CLUSTERED ([MenuId] ASC)
);

