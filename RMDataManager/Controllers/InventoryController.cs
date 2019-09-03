using System.Collections.Generic;
using System.Web.Http;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Controllers
{
	//[Authorize]
	public class InventoryController : ApiController
	{
		public List<InventoryModel> GetSalesReport()
		{
			InventoryData data = new InventoryData();
			return data.GetInventory();
		}

		public void Post(InventoryModel item)
		{
			InventoryData data = new InventoryData();

			data.SaveInventoryRecord(item);
		}
	}
}