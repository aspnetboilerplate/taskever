using System.Reflection;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Taskever.Localization.Resources;
using Taskever.Security.Roles;
using Taskever.Security.Tenants;
using Taskever.Security.Users;
using Taskever.Utils.Mail;

namespace Taskever.Startup
{
    [DependsOn(typeof(AbpZeroCoreModule))]

    public class TaskeverCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;
            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(TaskeverTenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(TaskeverRole);
            Configuration.Modules.Zero().EntityTypes.User = typeof(TaskeverUser);

            Configuration.MultiTenancy.IsEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Localization.Sources.Add(new TaskeverLocalizationSource());

            Configuration.Settings.Providers.Add<EmailSettingDefinitionProvider>();
        }
    }
}
