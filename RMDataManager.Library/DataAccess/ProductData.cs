using System.Collections.Generic;
using System.Linq;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.DataAccess
{
	public class ProductData
	{
		public List<ProductModel> GetProducts()
		{
			SqlDataAccess sql = new SqlDataAccess();
			var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");

			return output;
		}

		public ProductModel GetProductById(int productId)
		{
			SqlDataAccess sql = new SqlDataAccess();

			var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "RMData").FirstOrDefault();

			return output;
		}
	}
}