using System.Reflection;
using Abp.Modules;
using Taskever.Localization.Resources;
using Taskever.Utils.Mail;

namespace Taskever.Startup
{
    public class TaskeverCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Localization.Sources.Add(new TaskeverLocalizationSource());

            Configuration.Settings.Providers.Add<EmailSettingDefinitionProvider>();

        }
    }
}
