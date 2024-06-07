CREATE TABLE [dbo].[RoleMenu] (
    [RoleMenuId] INT IDENTITY (1, 1) NOT NULL,
    [RoleId]     INT NOT NULL,
    [MenuId]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleMenuId] ASC),
    CONSTRAINT [RoleMenu_MenuId_fkey] FOREIGN KEY ([MenuId]) REFERENCES [dbo].[Menu] ([MenuId]),
    CONSTRAINT [RoleMenu_RoleId_fkey] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId])
);

