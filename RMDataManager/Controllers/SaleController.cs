﻿using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Controllers
{
	[Authorize]
	public class SaleController : ApiController
	{
		[Authorize(Roles = "Manager,Admin")]
		[Route("GetSalesReport")]
		public List<SaleReportModel> GetSalesReport()
		{
			SaleData data = new SaleData();
			return data.GetSaleReport();
		}

		[Authorize(Roles = "Cashier")]
		public void Post(SaleModel sale)
		{
			SaleData data = new SaleData();
			string userId = RequestContext.Principal.Identity.GetUserId();

			data.SaveSale(sale, userId);
		}
	}
}