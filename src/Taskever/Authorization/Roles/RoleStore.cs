using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Taskever.Security.Roles;
using Taskever.Security.Users;

namespace Taskever.Authorization.Roles
{
    public class RoleStore : AbpRoleStore<TaskeverRole, TaskeverUser>
    {
        public RoleStore(
            IRepository<TaskeverRole> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository)
        {
        }
    }
}
