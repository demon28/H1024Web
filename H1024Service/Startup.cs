using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(H1024Service.Startup))]
namespace H1024Service
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
