using System.Threading.Tasks;
using RMDesktopUI.Models;

namespace RMDesktopUI.Library.Api
{
	public interface IAPIHelper
	{
		Task<AuthenticatedUser> Authenticate(string username, string password);

		Task GetLoggedInUserInfo(string token);
	}
}