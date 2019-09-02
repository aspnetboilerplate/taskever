using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.MultiTenancy;
using Taskever.Security.Users;

namespace Taskever.Security.Tenants
{
    public class TaskeverTenant : AbpTenant<TaskeverUser>
    {
        public TaskeverTenant()
        {
        }

        public TaskeverTenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
