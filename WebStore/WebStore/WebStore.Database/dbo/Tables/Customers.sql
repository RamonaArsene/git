CREATE TABLE [dbo].[Customers] (
    [CustomerId]       NUMERIC (18) NOT NULL,
    [CustomerName]     VARCHAR (50) NOT NULL,
    [CustomerDiscount] NCHAR (10)   NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC),
    CONSTRAINT [FK_Customers_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId])
);

