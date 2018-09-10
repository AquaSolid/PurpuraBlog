using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FilipBlog.Startup))]
namespace FilipBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
