CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId]      INT      NOT NULL,
    [RoleId]      INT      NOT NULL,
    [CreatedDate] DATETIME NULL,
    CONSTRAINT [AspNetUserRoles_pkey] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [AspNetUserRoles_RoleId_fkey] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]),
    CONSTRAINT [AspNetUserRoles_UserId_fkey] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

