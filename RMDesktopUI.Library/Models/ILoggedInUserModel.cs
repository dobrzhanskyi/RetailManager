using System;

namespace RMDesktopUI.Library.Models
{
	public interface ILoggedInUserModel
	{
		DateTime CreatedDate { get; set; }
		string EmailAdress { get; set; }
		string FirstName { get; set; }
		string Id { get; set; }
		string LastName { get; set; }
		string Token { get; set; }

		void ResetUserModel();
	}
}