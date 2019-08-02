﻿using System.Net.Http;
using System.Threading.Tasks;
using RMDesktopUI.Models;

namespace RMDesktopUI.Library.Api
{
	public interface IAPIHelper
	{
		HttpClient ApiClient { get; }

		Task<AuthenticatedUser> Authenticate(string username, string password);

		Task GetLoggedInUserInfo(string token);
	}
}