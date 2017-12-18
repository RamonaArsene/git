CREATE TABLE [dbo].[Country] (
    [Cod]         CHAR (2)        NOT NULL,
    [Latitudine]  DECIMAL (20, 8) NULL,
    [Longitudine] DECIMAL (20, 8) NULL,
    [Nume]        NVARCHAR (200)  NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([Cod] ASC)
);

