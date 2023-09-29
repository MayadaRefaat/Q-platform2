using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mr_bahaa.Startup))]
namespace mr_bahaa
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
