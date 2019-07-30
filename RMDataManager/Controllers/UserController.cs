using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Controllers
{
	[Authorize]
	public class UserController : ApiController
	{
		[HttpGet]
		public UserModel GetById()
		{
			//Getting id from logged user
			string userId = RequestContext.Principal.Identity.GetUserId();
			UserData userData = new UserData();

			return userData.GetUserById(userId).First();
		}
	}
}