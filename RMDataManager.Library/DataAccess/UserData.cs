﻿using System.Collections.Generic;
using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

namespace RMDataManager.Library.DataAccess
{
	public class UserData
	{
		public List<UserModel> GetUserById(string Id)
		{
			SqlDataAccess sql = new SqlDataAccess();
			var parameter = new { Id = Id };
			var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", parameter, "RMData");

			return output;
		}
	}
}