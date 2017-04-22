namespace Hotel.Data.DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        AppUserId = c.Long(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 100),
                        Lastname = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Mobile = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false),
                        RoleId = c.Long(),
                        CreatedBy = c.Long(),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(),
                    })
                .PrimaryKey(t => t.AppUserId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ManageItems = c.Boolean(nullable: false),
                        ManageItemCategory = c.Boolean(nullable: false),
                        ManageUsers = c.Boolean(nullable: false),
                        AccessItemLog = c.Boolean(nullable: false),
                        AccessGeneralLog = c.Boolean(nullable: false),
                        CreatedBy = c.Long(),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.ItemCategories",
                c => new
                    {
                        ItemCategoryId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.Long(),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(),
                    })
                .PrimaryKey(t => t.ItemCategoryId);
            
            CreateTable(
                "dbo.ItemLogs",
                c => new
                    {
                        ItemLogId = c.Long(nullable: false, identity: true),
                        ItemId = c.Long(),
                        TransactionType = c.String(),
                        Quantity = c.Long(nullable: false),
                        TotalAmount = c.Long(nullable: false),
                        CreatedBy = c.Long(),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(),
                    })
                .PrimaryKey(t => t.ItemLogId)
                .ForeignKey("dbo.Items", t => t.ItemId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Quantity = c.Long(nullable: false),
                        Amount = c.Long(nullable: false),
                        ItemCategoryId = c.Long(),
                        CreatedBy = c.Long(),
                        DateCreated = c.DateTime(nullable: false),
                        DateLastModified = c.DateTime(nullable: false),
                        LastModifiedBy = c.Long(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.ItemCategories", t => t.ItemCategoryId)
                .Index(t => t.ItemCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemLogs", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "ItemCategoryId", "dbo.ItemCategories");
            DropForeignKey("dbo.AppUsers", "RoleId", "dbo.Roles");
            DropIndex("dbo.Items", new[] { "ItemCategoryId" });
            DropIndex("dbo.ItemLogs", new[] { "ItemId" });
            DropIndex("dbo.AppUsers", new[] { "RoleId" });
            DropTable("dbo.Items");
            DropTable("dbo.ItemLogs");
            DropTable("dbo.ItemCategories");
            DropTable("dbo.Roles");
            DropTable("dbo.AppUsers");
        }
    }
}
