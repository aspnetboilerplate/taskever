using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Abp.Zero.EntityFramework;
using Taskever.Activities;
using Taskever.Friendships;
using Taskever.Security.Roles;
using Taskever.Security.Tenants;
using Taskever.Security.Users;
using Taskever.Tasks;

namespace Taskever.Infrastructure.EntityFramework.Data.Repositories.NHibernate
{
    public class TaskeverDbContext : AbpZeroDbContext<TaskeverTenant, TaskeverRole, TaskeverUser>
    {
        public virtual IDbSet<Friendship> Friendships { get; set; }
        public virtual IDbSet<Task> Tasks { get; set; }

        public TaskeverDbContext()
            : base("Taskever")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //TODO: Ignore base classes

            //modelBuilder.Entity<UserPermissionSetting>().ToTable("AbpPermissions");
            //modelBuilder.Entity<UserRole>().ToTable("AbpUserRoles");
            //modelBuilder.Entity<Setting>().ToTable("AbpSettings");
            //modelBuilder.Entity<TaskeverRole>().ToTable("AbpRoles");
            //modelBuilder.Entity<TaskeverTenant>().ToTable("AbpTenants");
            //modelBuilder.Entity<UserLogin>().ToTable("AbpUserLogins");

            //modelBuilder.Entity<UserRole>().ToTable("AbpUserRoles");

            //modelBuilder.Entity<TaskeverRole>().HasMany(r => r.Permissions).WithRequired().HasForeignKey(p => p.RoleId);

            ////modelBuilder.Entity<UserRole>().HasRequired(ur => ur.UserId);
            ////modelBuilder.Entity<UserRole>().HasRequired(ur => ur.RoleId);

            //modelBuilder.Entity<TaskeverUser>().ToTable("AbpUsers");

            //modelBuilder.Ignore<TaskeverUser>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<TaskeverUser>().ToTable("AbpUsers");
            modelBuilder.Entity<Activity>().ToTable("TeActivities")
                .Map<CreateTaskActivity>(m => m.Requires("ActivityType").HasValue(1))
                .Map<CompleteTaskActivity>(m => m.Requires("ActivityType").HasValue(2));

            //modelBuilder.Entity<CompleteTaskActivity>()
            modelBuilder.Entity<Friendship>().ToTable("TeFriendships");
            modelBuilder.Entity<Task>().ToTable("TeTasks");
            modelBuilder.Entity<UserFollowedActivity>().ToTable("TeUserFollowedActivities");
        }
    }
}