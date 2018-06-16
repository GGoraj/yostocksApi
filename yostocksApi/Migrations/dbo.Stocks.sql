CREATE TABLE [dbo].[Stocks] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Brand]               NVARCHAR (MAX) NOT NULL,
    [RemainingPercentage] FLOAT (53)     NOT NULL,
    [PriceWhenPurchased]  FLOAT (53)     NOT NULL,
    [DateGenerated]       NVARCHAR (MAX) NOT NULL,
    [TimeGenerated]       NVARCHAR (MAX) NOT NULL,
    [LogoImagePath]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Stocks] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_Stocks_Fragments] FOREIGN KEY ([Id]) REFERENCES [Fragments]([StockId])
);

