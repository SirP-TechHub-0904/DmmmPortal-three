using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DmmmPortal.Startup))]
namespace DmmmPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
