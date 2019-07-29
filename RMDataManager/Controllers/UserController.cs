using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Controllers
{
	[Authorize]
	public class UserController : ApiController
	{
		public List<UserModel> GetById()
		{
			//Getting id from logged user
			string userId = RequestContext.Principal.Identity.GetUserId();
			UserData userData = new UserData();

			return userData.GetUserById(userId);
		}
	}
}