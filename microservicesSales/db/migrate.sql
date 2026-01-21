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

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260121000151_Inicial'
)
BEGIN
    CREATE TABLE [PRODUCTS] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [Stock] int NOT NULL,
        [RowVersion] rowversion NOT NULL,
        CONSTRAINT [PK_PRODUCTS] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260121000151_Inicial'
)
BEGIN
    CREATE TABLE [SALES] (
        [Id] uniqueidentifier NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_SALES] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260121000151_Inicial'
)
BEGIN
    CREATE TABLE [SALEITEMS] (
        [Id] uniqueidentifier NOT NULL,
        [SaleId] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        [ProductName] nvarchar(100) NOT NULL,
        [Quantity] int NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_SALEITEMS] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_SALEITEMS_SALES_SaleId] FOREIGN KEY ([SaleId]) REFERENCES [SALES] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260121000151_Inicial'
)
BEGIN
    CREATE INDEX [IX_SALEITEMS_SaleId] ON [SALEITEMS] ([SaleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260121000151_Inicial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260121000151_Inicial', N'8.0.23');
END;
GO

COMMIT;
GO

