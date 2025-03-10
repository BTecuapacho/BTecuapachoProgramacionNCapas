using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PL_Web.Startup))]
namespace PL_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
