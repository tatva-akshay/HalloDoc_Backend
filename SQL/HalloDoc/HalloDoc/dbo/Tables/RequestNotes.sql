CREATE TABLE [dbo].[RequestNotes] (
    [RequestNotesId]      INT           IDENTITY (1, 1) NOT NULL,
    [RequestId]           INT           NOT NULL,
    [strMonth]            VARCHAR (20)  NULL,
    [intYear]             INT           NULL,
    [intDate]             INT           NULL,
    [PhysicianNotes]      VARCHAR (500) NULL,
    [AdminNotes]          VARCHAR (500) NULL,
    [CreatedBy]           INT           NOT NULL,
    [CreatedDate]         DATETIME      DEFAULT (getdate()) NOT NULL,
    [ModifiedBy]          INT           NULL,
    [ModifiedDate]        DATETIME      NULL,
    [IP]                  VARCHAR (20)  NULL,
    [AdministrativeNotes] VARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([RequestNotesId] ASC),
    CONSTRAINT [RequestNotes_CreatedBy_fkey] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [RequestNotes_ModifiedBy_fkey] FOREIGN KEY ([ModifiedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [RequestNotes_RequestId_fkey] FOREIGN KEY ([RequestId]) REFERENCES [dbo].[Request] ([RequestId])
);

