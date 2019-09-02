using System.Reflection;
using Abp.Modules;
using Abp.Users.Dto;
using Taskever.Mapping;

namespace Taskever.Startup
{
    public class TaskeverAppModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            TaskeverDtoMapper.Map();
            UserDtosMapper.Map();
        }
    }
}