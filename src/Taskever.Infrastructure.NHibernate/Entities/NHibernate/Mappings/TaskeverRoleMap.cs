using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.NHibernate.EntityMappings;
using Taskever.Security.Roles;
using DapperExtensions.Mapper;

namespace Taskever.Entities.NHibernate.Mappings
{
    public class TaskeverRoleMap : EntityMap<TaskeverRole>
    {
        public TaskeverRoleMap() : base("AbpRoles")
        {
            Id(x => x.Id);

            
            Map(x => x.Name);
            Map(x => x.DisplayName);
            HasMany(x => x.Permissions).KeyColumn("RoleId");

            this.MapAudited<TaskeverRole>();
            Polymorphism.Explicit();
        }
    }
}