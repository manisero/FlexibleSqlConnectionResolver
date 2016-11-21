INSERT INTO [dbo].[Order] ([Name])
VALUES ('Order');

DECLARE @OrderId INT = SCOPE_IDENTITY();

INSERT INTO [dbo].[OrderItem] ([OrderId], [Name])
VALUES (@OrderId, 'Item');
