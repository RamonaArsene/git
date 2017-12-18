CREATE TABLE [dbo].[Products] (
    [ProductId]    NUMERIC (18) NOT NULL,
    [ProductName]  VARCHAR (50) NOT NULL,
    [ProductPrice] NUMERIC (18) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

