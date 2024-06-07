CREATE TABLE [dbo].[PasswordReset] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Token]       VARCHAR (MAX) NOT NULL,
    [Email]       VARCHAR (30)  NOT NULL,
    [CreatedDate] DATETIME      DEFAULT (getdate()) NOT NULL,
    [IsUpdated]   BIT           DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

