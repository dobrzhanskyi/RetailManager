using System.Configuration;

namespace RMDataManager.Library
{
	public static class ConfigHelper
	{
		public static decimal GetTaxRate()
		{
			string rateText = ConfigurationManager.AppSettings["taxRate"];

			bool isValidTaxReate = decimal.TryParse(rateText, out decimal output);

			if (!isValidTaxReate)
			{
				throw new ConfigurationErrorsException("Tax rate is not set up properly");
			}
			return output;
		}
	}
}