using System.Configuration;

namespace RMDataManager.Library
{
	public class SqlDataAccess
	{
		public string GetConnectionString(string name)
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}
	}
}