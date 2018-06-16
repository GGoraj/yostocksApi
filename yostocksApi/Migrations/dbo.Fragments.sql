CREATE TABLE [dbo].[Fragments] (
    [Id]             INT        IDENTITY (1, 1) NOT NULL,
    [StockId]        INT        NOT NULL,
    [PercentValue]   FLOAT (53) NOT NULL,
    [YostocksUserId] INT        NOT NULL,
    CONSTRAINT [PK_dbo.Fragments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Fragments_dbo.Stocks_StockId] FOREIGN KEY ([StockId]) REFERENCES [dbo].[Stocks] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Fragments_dbo.YostocksUsers_YostocksUserId] FOREIGN KEY ([YostocksUserId]) REFERENCES [dbo].[YostocksUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_StockId]
    ON [dbo].[Fragments]([StockId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_YostocksUserId]
    ON [dbo].[Fragments]([YostocksUserId] ASC);

