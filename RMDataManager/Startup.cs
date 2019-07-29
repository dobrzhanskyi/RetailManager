using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(RMDataManager.Startup))]

namespace RMDataManager
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}