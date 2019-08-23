using System.Collections.Generic;
using System.Linq;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.DataAccess
{
	public class SaleData
	{
		//TODO: Make this Solid/DRY better
		public void SaveSale(SaleModel saleInfo, string cashierId)
		{
			List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
			ProductData products = new ProductData();
			var taxRate = ConfigHelper.GetTaxRate() / 100;

			foreach (var item in saleInfo.SaleDetails)
			{
				var detail = new SaleDetailDBModel
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity
				};

				//Get the info about product
				var productInfo = products.GetProductById(item.ProductId);

				if (productInfo == null)
				{
					throw new System.Exception($"The product Id of {item.ProductId} is not found in DB.");
				}

				detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

				if (productInfo.IsTaxable)
				{
					detail.Tax = (detail.PurchasePrice * taxRate);
				}

				details.Add(detail);
			}

			//Create the Sale model
			SaleDBModel sale = new SaleDBModel
			{
				SubTotal = details.Sum(x => x.PurchasePrice),
				Tax = details.Sum(x => x.Tax),
				CashierId = cashierId
			};

			sale.Total = sale.SubTotal + sale.Tax;
			//Save the sale model
			using (SqlDataAccess sql = new SqlDataAccess())
			{
				sql.StartTransaction("RMData");
				sql.SaveDataInTransaction("dbo.spSale_Insert", sale);

				sale.Id = sql.LoadDataInTransaction<int, dynamic>("spSale_Lookup", new { sale.CashierId, sale.SaleDate }).FirstOrDefault();

				foreach (var item in details)
				{
					item.SaleId = sale.Id;
					sql.SaveDataInTransaction("dbo.spSaleDetail_Insert", item);
				}
			}
		}
	}
}