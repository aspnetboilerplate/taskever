using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.NHibernate.EntityMappings;

namespace Taskever.Entities.NHibernate.Mappings
{
    public class TaskeverRolePermissionSettingMap : EntityMap<RolePermissionSetting, long>
    {
        public TaskeverRolePermissionSettingMap() : base("AbpPermissions")
        {
            Map(x => x.TenantId);
            Map(x => x.RoleId);
            Map(x => x.Name);
            Map(x => x.IsGranted);
            //Polymorphism.Explicit();
            //this.MapCreationAudited();
        }
    }
}
