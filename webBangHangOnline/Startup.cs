using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(webBangHangOnline.Startup))]
namespace webBangHangOnline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
