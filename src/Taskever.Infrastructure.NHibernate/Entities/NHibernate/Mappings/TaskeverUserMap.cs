using System;
using System.Linq.Expressions;
using Abp.Authorization;
using Abp.NHibernate.EntityMappings;
using Taskever.Security.Users;

namespace Taskever.Entities.NHibernate.Mappings
{
    public class TaskeverUserMap : EntityMap<TaskeverUser, long>
    {
        public TaskeverUserMap() : base("AbpUsers")
        {
            Id(x => x.Id);

            HasMany(x => x.Permissions).KeyColumn("UserId");
            //HasMany(x => x.Claims).KeyColumn("UserId");
            //HasMany(x => x.Roles).KeyColumn("UserId");
            //HasMany(x => x.Logins).KeyColumn("UserId");
            //HasMany( x => x.Settings).KeyColumn("UserId");

            Map(x => x.ProfileImage);
            Map(x => (object)x.TenantId);
            Map(x => x.UserName);
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.EmailAddress);
            Map(x => (object)x.IsEmailConfirmed);
            Map(x => x.EmailConfirmationCode);
            Map(x => x.Password);
            Map(x => x.PasswordResetCode);
            Polymorphism.Explicit();

            this.MapFullAudited();
        }
    }
}
