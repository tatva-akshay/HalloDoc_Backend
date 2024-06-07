CREATE TABLE [dbo].[AspNetUsers] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (256) NOT NULL,
    [PasswordHash] NVARCHAR (MAX) NULL,
    [Email]        NVARCHAR (256) NULL,
    [Phonenumber]  NVARCHAR (20)  NULL,
    [IP]           NVARCHAR (20)  NULL,
    [CreatedDate]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [ModifiedDate] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

