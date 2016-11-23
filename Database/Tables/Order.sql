CREATE TABLE [dbo].[Order] (
    [OrderId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (100) NOT NULL CONSTRAINT [UQ_Order_Name] UNIQUE,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderId] ASC)
);
