using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;

namespace Taskever.Startup
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(TaskeverCoreModule))]
    public class TaskeverDataModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}