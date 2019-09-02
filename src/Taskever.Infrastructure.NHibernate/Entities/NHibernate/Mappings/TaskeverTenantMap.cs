using Abp.NHibernate.EntityMappings;
using Taskever.Security.Tenants;

namespace Taskever.Entities.NHibernate.Mappings
{
    public class TaskeverTenantMap : EntityMap<TaskeverTenant>
    {
        public TaskeverTenantMap() : base("AbpTenants")
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.ConnectionString);
            Map(x => x.EditionId);

            

            this.MapCreationAudited();
        }
    }
}
