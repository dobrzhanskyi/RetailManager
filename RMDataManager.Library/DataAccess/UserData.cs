using System.Collections.Generic;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.DataAccess
{
	public class UserData
	{
		public List<UserModel> GetUserById(string Id)
		{
			SqlDataAccess sql = new SqlDataAccess();
			var p = new { Id = Id };

			var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", p, "RMData");

			return output;
		}
	}
}