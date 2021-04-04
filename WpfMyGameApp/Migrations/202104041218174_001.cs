namespace WpfMyGameApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Racks");
        }
    }
}
