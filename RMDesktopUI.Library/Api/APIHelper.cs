﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RMDesktopUI.Library.Models;
using RMDesktopUI.Models;

namespace RMDesktopUI.Library.Api
{
	public class APIHelper : IAPIHelper
	{
		#region Private Fields

		private readonly ILoggedInUserModel _loggedInUser;
		private HttpClient _apiClient;

		#endregion Private Fields

		#region Public Constructors

		public APIHelper(ILoggedInUserModel loggedUser)
		{
			InitializeClient();
			_loggedInUser = loggedUser;
		}

		#endregion Public Constructors

		#region Public Properties

		public HttpClient ApiClient
		{
			get
			{
				return _apiClient;
			}
		}

		#endregion Public Properties

		#region Public Methods

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

		public void LogOffUser()
		{
			_apiClient.DefaultRequestHeaders.Clear();
		}
		public async Task GetLoggedInUserInfo(string token)
		{
			_apiClient.DefaultRequestHeaders.Clear();
			_apiClient.DefaultRequestHeaders.Accept.Clear();
			_apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			_apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

			using (HttpResponseMessage response = await _apiClient.GetAsync("/api/User"))
			{
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
					_loggedInUser.CreatedDate = result.CreatedDate;
					_loggedInUser.EmailAdress = result.EmailAdress;
					_loggedInUser.FirstName = result.FirstName;
					_loggedInUser.Id = result.Id;
					_loggedInUser.LastName = result.LastName;
					_loggedInUser.Token = token;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}
		}

		#endregion Public Methods

		#region Private Methods

		private void InitializeClient()
		{
			string api = ConfigurationManager.AppSettings["api"];

			_apiClient = new HttpClient
			{
				BaseAddress = new System.Uri(api),
			};
			_apiClient.DefaultRequestHeaders.Accept.Clear();
			_apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		#endregion Private Methods
	}
}