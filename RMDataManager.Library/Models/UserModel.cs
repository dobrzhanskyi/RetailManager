using System;

namespace RMDataManager.Library.Models
{
	public class UserModel
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAdress { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}