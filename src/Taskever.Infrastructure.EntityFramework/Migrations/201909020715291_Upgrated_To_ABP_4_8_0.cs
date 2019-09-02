namespace Taskever.Infrastructure.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upgrated_To_ABP_4_8_0 : DbMigration
    {
        public override void Up()
        {
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
                .ForeignKey("dbo.TeTasks", t => t.TaskId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.TeTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        AssignedUserId = c.Long(),
                        Priority = c.Byte(nullable: false),
                        Privacy = c.Byte(nullable: false),
                        State = c.Byte(nullable: false),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeFriendships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        FriendUserId = c.Long(nullable: false),
                        PairFriendshipId = c.Int(nullable: false),
                        FollowActivities = c.Boolean(nullable: false),
                        CanAssignTask = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        LastVisitTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeFriendships", t => t.PairFriendshipId)
                .Index(t => t.PairFriendshipId);
            
            CreateTable(
                "dbo.TeUserFollowedActivities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsActor = c.Boolean(nullable: false),
                        IsRelated = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        Activity_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeActivities", t => t.Activity_Id)
                .Index(t => t.Activity_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeUserFollowedActivities", "Activity_Id", "dbo.TeActivities");
            DropForeignKey("dbo.TeFriendships", "PairFriendshipId", "dbo.TeFriendships");
            DropForeignKey("dbo.TeActivities", "TaskId", "dbo.TeTasks");
            DropIndex("dbo.TeUserFollowedActivities", new[] { "Activity_Id" });
            DropIndex("dbo.TeFriendships", new[] { "PairFriendshipId" });
            DropIndex("dbo.TeActivities", new[] { "TaskId" });
            DropTable("dbo.TeUserFollowedActivities");
            DropTable("dbo.TeFriendships");
            DropTable("dbo.TeTasks");
            DropTable("dbo.TeActivities");
        }
    }
}
