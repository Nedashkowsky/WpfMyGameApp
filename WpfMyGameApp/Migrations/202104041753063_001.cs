namespace WpfMyGameApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kvm Modules",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Name = c.String(maxLength: 127, storeType: "nvarchar"),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Racks",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        Name = c.String(maxLength: 127, storeType: "nvarchar"),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Servers",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CPUs = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                        Storage = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Name = c.String(maxLength: 127, storeType: "nvarchar"),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Size = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Name = c.String(maxLength: 127, storeType: "nvarchar"),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.NetworkSwitches",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Count = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Name = c.String(maxLength: 127, storeType: "nvarchar"),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NetworkSwitches");
            DropTable("dbo.Storages");
            DropTable("dbo.Servers");
            DropTable("dbo.Racks");
            DropTable("dbo.Kvm Modules");
        }
    }
}
