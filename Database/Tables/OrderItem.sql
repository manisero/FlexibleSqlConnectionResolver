CREATE TABLE [dbo].[OrderItem] (
    [OrderItemId] INT            IDENTITY (1, 1) NOT NULL,
	[OrderId]     INT            NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED ([OrderItemId] ASC),
    CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId]),
    CONSTRAINT [UQ_OrderItem_Name] UNIQUE ([Name])
);
