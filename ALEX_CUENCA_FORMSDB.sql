IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Forms] (
    [Id] int NOT NULL IDENTITY,
    [OptionId] int NULL,
    [Name] nvarchar(300) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Forms] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FormTypeData] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_FormTypeData] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FormsAnswers] (
    [Id] int NOT NULL IDENTITY,
    [FormId] int NOT NULL,
    [Name] nvarchar(300) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_FormsAnswers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FormsAnswers_Forms_FormId] FOREIGN KEY ([FormId]) REFERENCES [Forms] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FormInputs] (
    [Id] int NOT NULL IDENTITY,
    [Label] nvarchar(300) NOT NULL,
    [FormTypeDataId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [FormId] int NULL,
    CONSTRAINT [PK_FormInputs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FormInputs_FormTypeData_FormTypeDataId] FOREIGN KEY ([FormTypeDataId]) REFERENCES [FormTypeData] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FormInputs_Forms_FormId] FOREIGN KEY ([FormId]) REFERENCES [Forms] ([Id])
);
GO

CREATE TABLE [FormInputAnswers] (
    [Id] int NOT NULL IDENTITY,
    [Label] nvarchar(300) NOT NULL,
    [Value] nvarchar(300) NULL,
    [FormTypeDataId] int NOT NULL,
    [FormAnswerId] int NULL,
    CONSTRAINT [PK_FormInputAnswers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_FormInputAnswers_FormTypeData_FormTypeDataId] FOREIGN KEY ([FormTypeDataId]) REFERENCES [FormTypeData] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FormInputAnswers_FormsAnswers_FormAnswerId] FOREIGN KEY ([FormAnswerId]) REFERENCES [FormsAnswers] ([Id])
);
GO

CREATE INDEX [IX_FormInputAnswers_FormAnswerId] ON [FormInputAnswers] ([FormAnswerId]);
GO

CREATE INDEX [IX_FormInputAnswers_FormTypeDataId] ON [FormInputAnswers] ([FormTypeDataId]);
GO

CREATE INDEX [IX_FormInputs_FormId] ON [FormInputs] ([FormId]);
GO

CREATE INDEX [IX_FormInputs_FormTypeDataId] ON [FormInputs] ([FormTypeDataId]);
GO

CREATE INDEX [IX_FormsAnswers_FormId] ON [FormsAnswers] ([FormId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240703193526_initial_commit', N'8.0.5');
GO

COMMIT;
GO

