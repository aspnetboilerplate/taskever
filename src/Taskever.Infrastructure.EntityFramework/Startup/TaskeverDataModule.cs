using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;

namespace Taskever.Infrastructure.EntityFramework.Startup
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule))]
    public class TaskeverDataModule : AbpModule
    {

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}