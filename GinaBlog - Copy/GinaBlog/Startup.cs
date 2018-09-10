using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GinaBlog.Startup))]
namespace GinaBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
