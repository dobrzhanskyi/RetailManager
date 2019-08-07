CREATE PROCEDURE [dbo].[spProduct_GetAll]

AS
begin
	SELECT Id, ProductName, [Description], RetailPrice, QuantityInStock,IsTaxable
	FROM dbo.Product
	ORDER BY ProductName;
end