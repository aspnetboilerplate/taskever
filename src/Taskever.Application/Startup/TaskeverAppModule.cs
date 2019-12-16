using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;
using Taskever.Mapping;

namespace Taskever.Startup
{
    [DependsOn(typeof(TaskeverCoreModule), typeof(AbpAutoMapperModule))]
    public class TaskeverAppModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                new UserDtosMapper().Map(cfg);
                new TaskeverDtoMapper().Map(cfg);
            });
        }
    }
}