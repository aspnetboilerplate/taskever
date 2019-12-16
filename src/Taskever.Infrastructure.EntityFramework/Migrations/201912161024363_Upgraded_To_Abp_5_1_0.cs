namespace Taskever.Infrastructure.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upgraded_To_Abp_5_1_0 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Friendships", newName: "TeFriendships");
            RenameTable(name: "dbo.Tasks", newName: "TeTasks");
            DropIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
            CreateTable(
                "dbo.TeActivities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AssignedUserId = c.Long(nullable: false),
                        TaskId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        ActivityType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AbpUsers", t => t.AssignedUserId)
                .ForeignKey("dbo.TeTasks", t => t.TaskId)
                .ForeignKey("dbo.AbpUsers", t => t.CreatorUserId)
                .Index(t => t.AssignedUserId)
                .Index(t => t.TaskId)
                .Index(t => t.CreatorUserId);
            
            CreateTable(
                "dbo.TeUserFollowedActivities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ActivityId = c.Long(nullable: false),
                        IsActor = c.Boolean(nullable: false),
                        IsRelated = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeActivities", t => t.ActivityId)
                .ForeignKey("dbo.AbpUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ActivityId);
            
            CreateIndex("dbo.AbpSettings", "TenantId");
            CreateIndex("dbo.AbpBackgroundJobs", new[] { "Priority", "TryCount", "NextTryTime" });
            CreateIndex("dbo.AbpBackgroundJobs", "IsAbandoned", name: "IX_IsAbandoned_NextTryTime");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeUserFollowedActivities", "UserId", "dbo.AbpUsers");
            DropForeignKey("dbo.TeUserFollowedActivities", "ActivityId", "dbo.TeActivities");
            DropForeignKey("dbo.TeActivities", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.TeActivities", "TaskId", "dbo.TeTasks");
            DropForeignKey("dbo.TeActivities", "AssignedUserId", "dbo.AbpUsers");
            DropIndex("dbo.TeUserFollowedActivities", new[] { "ActivityId" });
            DropIndex("dbo.TeUserFollowedActivities", new[] { "UserId" });
            DropIndex("dbo.AbpBackgroundJobs", "IX_IsAbandoned_NextTryTime");
            DropIndex("dbo.AbpBackgroundJobs", new[] { "Priority", "TryCount", "NextTryTime" });
            DropIndex("dbo.AbpSettings", new[] { "TenantId" });
            DropIndex("dbo.TeActivities", new[] { "CreatorUserId" });
            DropIndex("dbo.TeActivities", new[] { "TaskId" });
            DropIndex("dbo.TeActivities", new[] { "AssignedUserId" });
            DropTable("dbo.TeUserFollowedActivities");
            DropTable("dbo.TeActivities");
            CreateIndex("dbo.AbpBackgroundJobs", new[] { "IsAbandoned", "NextTryTime" });
            RenameTable(name: "dbo.TeTasks", newName: "Tasks");
            RenameTable(name: "dbo.TeFriendships", newName: "Friendships");
        }
    }
}
