CREATE TABLE [dbo].[Orders] (
    [OderId]     NUMERIC (18) NOT NULL,
    [OrderDate]  DATE         NOT NULL,
    [CustomerId] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OderId] ASC),
    CONSTRAINT [FK_Orders_Orders] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Orders]
    ON [dbo].[Orders]([OderId] ASC);

