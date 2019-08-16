using System.Threading.Tasks;
using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
	public interface ISaleEndpoint
	{
		Task PostSale(SaleModel sale);
	}
}