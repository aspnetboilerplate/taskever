
using System.Reflection;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Modules;
using Abp.Web.Mvc.Resources;
using Taskever.Startup;
using Taskever.Tasks;

namespace Taskever.Web.Mvc
{
    [DependsOn(typeof(TaskeverDataModule))]
    public class TaskeverWebMvcModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            WebResourceHelper.ExposeEmbeddedResources("Taskever/Er/Test", typeof(TaskAppService).Assembly, "Taskever.Test");

            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}