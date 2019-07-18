﻿using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RMDesktopUI.Models;

namespace RMDesktopUI.Helpers
{
	public class APIHelper : IAPIHelper
	{
		private HttpClient _apiClient;

		public APIHelper()
		{
			InitializeClient();
		}

		private void InitializeClient()
		{
			string api = ConfigurationManager.AppSettings["api"];

			_apiClient = new HttpClient();
			_apiClient.BaseAddress = new System.Uri(api);
			_apiClient.DefaultRequestHeaders.Accept.Clear();
			_apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<AuthenticatedUser> Authenticate(string username, string password)
		{
			var data = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string,string>("grant_type","password"),
				new KeyValuePair<string,string>("username",username),
				new KeyValuePair<string,string>("password",password)
			});

			using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
					return result;
				}
				else
				{
					throw new System.Exception(response.ReasonPhrase);
				}
			}
		}
	}
}