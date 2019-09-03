CREATE PROCEDURE [dbo].[spSale_SaleReport]

AS
begin
	set nocount on;

	Select [s].[Id], [s].[CashierId], [s].[SaleDate], [s].[SubTotal], [s].[Tax], [s].[Total], [u].[FirstName], [u].[LastName], [u].[EmailAdress]
	from dbo.Sale s
	INNER JOIN dbo.[User] u on s.CashierId = u.Id
end