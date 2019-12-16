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
        public virtual IDbSet<Activity> Activities { get; set; }

        public TaskeverDbContext()
            : base("Taskever")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
            modelBuilder.Entity<TaskeverUser>().ToTable("AbpUsers");
            modelBuilder.Entity<Activity>().ToTable("TeActivities")
                .Map<CreateTaskActivity>(m => m.Requires("ActivityType").HasValue((int)ActivityType.CreateTask))
                .Map<CompleteTaskActivity>(m => m.Requires("ActivityType").HasValue((int)ActivityType.CompleteTask));

            modelBuilder.Entity<Friendship>().ToTable("TeFriendships");
            modelBuilder.Entity<Task>().ToTable("TeTasks");
            modelBuilder.Entity<UserFollowedActivity>().ToTable("TeUserFollowedActivities");
        }
    }
}