CREATE TABLE [dbo].[Role] (
    [RoleId]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50)  NOT NULL,
    [AccountType]  SMALLINT      NOT NULL,
    [CreatedBy]    VARCHAR (128) NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL,
    [ModifiedBy]   VARCHAR (128) NULL,
    [ModifiedDate] DATETIME      NULL,
    [IP]           VARCHAR (20)  NULL,
    [IsDeleted]    BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

