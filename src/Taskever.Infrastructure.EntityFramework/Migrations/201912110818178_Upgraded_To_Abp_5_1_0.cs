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
                        IsActor = c.Boolean(nullable: false),
                        IsRelated = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        Activity_Id = c.Long(),
                        User_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeActivities", t => t.Activity_Id)
                .ForeignKey("dbo.AbpUsers", t => t.User_Id)
                .Index(t => t.Activity_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeUserFollowedActivities", "User_Id", "dbo.AbpUsers");
            DropForeignKey("dbo.TeUserFollowedActivities", "Activity_Id", "dbo.TeActivities");
            DropForeignKey("dbo.TeActivities", "CreatorUserId", "dbo.AbpUsers");
            DropForeignKey("dbo.TeActivities", "TaskId", "dbo.TeTasks");
            DropForeignKey("dbo.TeActivities", "AssignedUserId", "dbo.AbpUsers");
            DropIndex("dbo.TeUserFollowedActivities", new[] { "User_Id" });
            DropIndex("dbo.TeUserFollowedActivities", new[] { "Activity_Id" });
            DropIndex("dbo.TeActivities", new[] { "CreatorUserId" });
            DropIndex("dbo.TeActivities", new[] { "TaskId" });
            DropIndex("dbo.TeActivities", new[] { "AssignedUserId" });
            DropTable("dbo.TeUserFollowedActivities");
            DropTable("dbo.TeActivities");
            RenameTable(name: "dbo.TeTasks", newName: "Tasks");
            RenameTable(name: "dbo.TeFriendships", newName: "Friendships");
        }
    }
}
