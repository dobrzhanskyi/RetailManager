using System.Web.Http;
using Microsoft.AspNet.Identity;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Controllers
{
	[Authorize]
	public class SaleController : ApiController
	{
		public void Post(SaleModel sale)
		{
			SaleData data = new SaleData();
			string userId = RequestContext.Principal.Identity.GetUserId();

			data.SaveSale(sale, userId);
		}
	}
}