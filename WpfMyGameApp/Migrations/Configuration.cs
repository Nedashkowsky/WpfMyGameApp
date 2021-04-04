namespace WpfMyGameApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WpfMyGameApp.DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(WpfMyGameApp.DB context)
        {
            //  This method will be called after migrating to the latest version.

            context.Racks.AddIfNameNotExists(
                new Entities.Rack()
                { 
                    Name = "IBM",
                    Price = 300,
                    Capacity = 400,
                    Count = 6
                },
                 new Entities.Rack()
                 {
                     Name = "APC",
                     Price = 400,
                     Capacity = 550,
                     Count = 6
                 });

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
