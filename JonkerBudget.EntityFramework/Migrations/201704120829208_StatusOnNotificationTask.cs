namespace SGNotify.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusOnNotificationTask : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.NotificationTask", "StatusId");
            AddForeignKey("dbo.NotificationTask", "StatusId", "dbo.Status", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationTask", "StatusId", "dbo.Status");
            DropIndex("dbo.NotificationTask", new[] { "StatusId" });
        }
    }
}
