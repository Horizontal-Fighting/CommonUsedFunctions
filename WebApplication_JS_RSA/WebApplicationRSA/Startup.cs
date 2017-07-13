using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplicationRSA.Startup))]
namespace WebApplicationRSA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
