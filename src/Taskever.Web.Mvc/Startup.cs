using Abp.Owin;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Taskever.Web.Mvc.Startup))]
namespace Taskever.Web.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAbp();

            ConfigureAuth(app);
        }
    }
}
