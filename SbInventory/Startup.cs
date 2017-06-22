using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SbInventory.Startup))]
namespace SbInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
