using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Taskever.Activities;
using Taskever.Friendships;
using Taskever.Tasks;
using Taskever.Users;
using Taskever.Web.Dependency.Installers;

namespace Taskever.Web.Startup
{
    [DependsOn(typeof(AbpWebApiModule))]
    public class TaskeverWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.IocContainer.Install(new TaskeverWebInstaller());
            CreateWebApiProxiesForServices();
        }

        private void CreateWebApiProxiesForServices()
        {
            //TODO: must be able to exclude/include all methods option

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .For<ITaskeverUserAppService>("taskever/user")
                .Build();

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .For<ITaskAppService>("taskever/task")
                .Build();

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .For<IFriendshipAppService>("taskever/friendship")
                .Build();

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .For<IUserActivityAppService>("taskever/userActivity")
                .Build();
        }
    }
}