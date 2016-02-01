using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SriLankanLifeVS.Startup))]
namespace SriLankanLifeVS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
