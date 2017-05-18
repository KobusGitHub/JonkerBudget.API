namespace JonkerBudget.EntityFramework.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditDetail",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PropertyName = c.String(unicode: false),
                        OldValue = c.String(unicode: false),
                        NewValue = c.String(unicode: false),
                        Audit_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Audit", t => t.Audit_Id)
                .Index(t => t.Audit_Id);
            
            CreateTable(
                "dbo.Audit",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Username = c.String(unicode: false),
                        TimeStampUtc = c.DateTime(nullable: false, precision: 0),
                        TableName = c.String(unicode: false),
                        ObjectId = c.String(maxLength: 128, storeType: "nvarchar"),
                        ActionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ObjectId);
            
            CreateTable(
                "dbo.EscalationDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PeriodInMinutes = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        SequenceNo = c.Int(nullable: false),
                        NotificationTaskId = c.Int(nullable: false),
                        DateEscalatedUtc = c.DateTime(precision: 0),
                        DateCreatedUtc = c.DateTime(nullable: false, precision: 0),
                        DateUpdatedUtc = c.DateTime(nullable: false, precision: 0),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationTask", t => t.NotificationTaskId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.NotificationTaskId);
            
            CreateTable(
                "dbo.NotificationTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        CreatedBy = c.String(maxLength: 100, storeType: "nvarchar"),
                        Description = c.String(maxLength: 1000, storeType: "nvarchar"),
                        Priority = c.Int(nullable: false),
                        ImpactDescription = c.String(maxLength: 1000, storeType: "nvarchar"),
                        SystemInfo = c.String(maxLength: 100, storeType: "nvarchar"),
                        AssignedToUserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        DateCreatedUtc = c.DateTime(nullable: false, precision: 0),
                        DateUpdatedUtc = c.DateTime(nullable: false, precision: 0),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.AssignedToUserId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.StatusId)
                .Index(t => t.AssignedToUserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        FirstName = c.String(unicode: false),
                        Surname = c.String(unicode: false),
                        IsAdUser = c.Boolean(nullable: false),
                        PlayerId = c.String(unicode: false),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.NotificationTaskUpdate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationTaskId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        Comment = c.String(maxLength: 2000, storeType: "nvarchar"),
                        DateCreatedUtc = c.DateTime(nullable: false, precision: 0),
                        DateUpdatedUtc = c.DateTime(nullable: false, precision: 0),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.NotificationTask", t => t.NotificationTaskId)
                .Index(t => t.NotificationTaskId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        DateCreatedUtc = c.DateTime(nullable: false, precision: 0),
                        DateUpdatedUtc = c.DateTime(nullable: false, precision: 0),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserManager",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128, storeType: "nvarchar"),
                        ManagerId = c.String(maxLength: 128, storeType: "nvarchar"),
                        DateCreatedUtc = c.DateTime(nullable: false, precision: 0),
                        DateUpdatedUtc = c.DateTime(nullable: false, precision: 0),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ManagerId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ManagerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserManager", "UserId", "dbo.User");
            DropForeignKey("dbo.UserManager", "ManagerId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.EscalationDetail", "UserId", "dbo.User");
            DropForeignKey("dbo.NotificationTask", "StatusId", "dbo.Status");
            DropForeignKey("dbo.NotificationTaskUpdate", "NotificationTaskId", "dbo.NotificationTask");
            DropForeignKey("dbo.NotificationTaskUpdate", "UserId", "dbo.User");
            DropForeignKey("dbo.EscalationDetail", "NotificationTaskId", "dbo.NotificationTask");
            DropForeignKey("dbo.NotificationTask", "AssignedToUserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.AuditDetail", "Audit_Id", "dbo.Audit");
            DropIndex("dbo.UserManager", new[] { "ManagerId" });
            DropIndex("dbo.UserManager", new[] { "UserId" });
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.NotificationTaskUpdate", new[] { "UserId" });
            DropIndex("dbo.NotificationTaskUpdate", new[] { "NotificationTaskId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.NotificationTask", new[] { "AssignedToUserId" });
            DropIndex("dbo.NotificationTask", new[] { "StatusId" });
            DropIndex("dbo.EscalationDetail", new[] { "NotificationTaskId" });
            DropIndex("dbo.EscalationDetail", new[] { "UserId" });
            DropIndex("dbo.Audit", new[] { "ObjectId" });
            DropIndex("dbo.AuditDetail", new[] { "Audit_Id" });
            DropTable("dbo.UserManager",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                });
            DropTable("dbo.Role");
            DropTable("dbo.Status",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                });
            DropTable("dbo.NotificationTaskUpdate",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                });
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.NotificationTask",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                });
            DropTable("dbo.EscalationDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "globalFilter_IsDeleted", "EntityFramework.Filters.FilterDefinition" },
                });
            DropTable("dbo.Audit");
            DropTable("dbo.AuditDetail");
        }
    }
}
