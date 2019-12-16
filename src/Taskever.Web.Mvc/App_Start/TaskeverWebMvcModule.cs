using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Logging;
using Abp.Modules;
using Abp.Web.Mvc;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Microsoft.Owin.Security;
using Taskever.Startup;
using Taskever.Web.Mvc.App_Start;
using Taskever.Web.Startup;

namespace Taskever.Web.Mvc
{
    [DependsOn(
        typeof(TaskeverDataModule),
        typeof(TaskeverAppModule),
        typeof(TaskeverWebApiModule),
        //typeof(AbpHangfireModule), - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
        typeof(AbpWebMvcModule))]
    public class TaskeverWebMvcModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Enable database based localization
            //Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<AbpProjectNameNavigationProvider>();

            //Configure Hangfire - ENABLE TO USE HANGFIRE INSTEAD OF DEFAULT JOB MANAGER
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseSqlServerStorage("Default");
            //});
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component
                    .For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .LifestyleTransient()
            );

            //WebResourceHelper.ExposeEmbeddedResources("Taskever/Er/Test", typeof(TaskAppService).Assembly, "Taskever.Test");

            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                var logger = IocManager.IocContainer.Resolve<ILogger>();
                logger.Log(LogSeverity.Error, eventArgs.Exception.ToString());
                //Debug.WriteLine(eventArgs.Exception.ToString());
            };
        }
    }
}