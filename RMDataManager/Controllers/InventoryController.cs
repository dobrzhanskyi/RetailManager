using System.Collections.Generic;
using System.Web.Http;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Controllers
{
	[Authorize]
	public class InventoryController : ApiController
	{
		[Authorize(Roles = "Manager,Admin")]
		public List<InventoryModel> GetSalesReport()
		{
			InventoryData data = new InventoryData();
			return data.GetInventory();
		}

		[Authorize(Roles = "Admin")]
		public void Post(InventoryModel item)
		{
			InventoryData data = new InventoryData();

			data.SaveInventoryRecord(item);
		}
	}
}