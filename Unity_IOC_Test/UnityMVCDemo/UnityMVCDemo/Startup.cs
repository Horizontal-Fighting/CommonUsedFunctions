using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UnityMVCDemo.Startup))]
namespace UnityMVCDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
