CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailsId] NUMERIC (18) NOT NULL,
    [ProductId]      NUMERIC (18) NOT NULL,
    [NoItems]        NUMERIC (18) NOT NULL,
    [OrderId]        NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([OrderDetailsId] ASC),
    CONSTRAINT [FK_OrderDetails_OrderDetails1] FOREIGN KEY ([OrderDetailsId]) REFERENCES [dbo].[OrderDetails] ([OrderDetailsId])
);

