using System.Collections.Generic;
using System.Web.Http;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Controllers
{
	[Authorize]
	public class ProductController : ApiController
	{
		public List<ProductModel> Get()
		{
			ProductData data = new ProductData();

			return data.GetProducts();
		}
	}
}