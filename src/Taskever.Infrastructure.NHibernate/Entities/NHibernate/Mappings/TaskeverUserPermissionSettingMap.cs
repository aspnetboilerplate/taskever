using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.NHibernate.EntityMappings;

namespace Taskever.Entities.NHibernate.Mappings
{
    public class TaskeverUserPermissionSettingMap : EntityMap<UserPermissionSetting, long>
    {
        public TaskeverUserPermissionSettingMap() : base("AbpPermissions")
        {
            Map(x => x.TenantId);
            Map(x => x.UserId);
            Map(x => x.Name);
            Map(x => x.IsGranted);
            Polymorphism.Explicit();

            this.MapCreationAudited();
        }
    }
}
