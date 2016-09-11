using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5Products.Startup))]
namespace MVC5Products
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
