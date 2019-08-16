using System;
using System.Net.Http;
using System.Threading.Tasks;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
	public class SaleEndpoint : ISaleEndpoint
	{
		private readonly IAPIHelper _apiHelper;

		public SaleEndpoint(IAPIHelper aPIHelper)
		{
			_apiHelper = aPIHelper;
		}

		public async Task PostSale(SaleModel sale)
		{
			using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
			{
				if (response.IsSuccessStatusCode)
				{
					//TODO: Log successfull call?
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}
	}
}