using System.Configuration;

namespace RMDesktopUI.Library.Helpers
{
	public class ConfigHelper : IConfigHelper
	{
		public decimal GetTaxRate()
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