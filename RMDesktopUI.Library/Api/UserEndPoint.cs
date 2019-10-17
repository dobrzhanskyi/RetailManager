using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
	public class UserEndPoint : IUserEndPoint
	{
		private readonly IAPIHelper _apiHelper;

		public UserEndPoint(IAPIHelper apiHelper)
		{
			_apiHelper = apiHelper;
		}

		public async Task<List<UserModel>> GetAll()
		{
			using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/User/Admin/GetAllUsers"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<List<UserModel>>();
					return result;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}
	}
}