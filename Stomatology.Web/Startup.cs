using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stomatology.Web.Startup))]
namespace Stomatology.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
