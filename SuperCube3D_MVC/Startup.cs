using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SuperCube3D_MVC.Startup))]
namespace SuperCube3D_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
