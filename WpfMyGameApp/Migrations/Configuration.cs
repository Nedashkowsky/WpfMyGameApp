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
					Name = "Rittal",
					Price = 120,
					Capacity = 500,
					Count = 1
				},
				 new Entities.Rack()
				 {
					 Name = "APC",
					 Price = 100,
					 Capacity = 400,
					 Count = 1
				 },
				 new Entities.Rack()
				 {
					 Name = "HyperLine",
					 Price = 80,
					 Capacity = 400,
					 Count = 1
				 });

			context.Servers.AddIfNameNotExists(
				new Entities.Server()
				{
					Name = "Lenovo",
					Price = 20,
					Weight = 10,
					Size = 1,
					CPUs = 2
				},
				 new Entities.Server()
				 {
					 Name = "Dell",
					 Price = 30,
					 Weight = 15,
					 Size = 1,
					 CPUs = 4
				 },
				 new Entities.Server()
				 {
					 Name = "Fujitsu",
					 Price = 40,
					 Weight = 20,
					 Size = 2,
					 CPUs = 6
				 },
				 new Entities.Server()
				 {
					 Name = "HP",
					 Price = 35,
					 Weight = 25,
					 Size = 3,
					 CPUs = 6
				 });

			context.Kvms.AddIfNameNotExists(
				new Entities.KvmConsole()
				{
					Name = "APC",
					Price = 50,
					Weight = 15,
					Count = 1
				},
				 new Entities.KvmConsole()
				 {
					 Name = "HP",
					 Price = 90,
					 Weight = 15,
					 Count = 1
				 },
				 new Entities.KvmConsole()
				 {
					 Name = "IBM",
					 Price = 70,
					 Weight = 10,
					 Count = 1
				 });

			context.Switches.AddIfNameNotExists(
				new Entities.NetworkSwitch()
				{
					Name = "Cisko",
					Price = 200,
					Weight = 5,
					Count = 1
				},
				 new Entities.NetworkSwitch()
				 {
					 Name = "D-Link",
					 Price = 300,
					 Weight = 10,
					 Count = 1
				 },
				 new Entities.NetworkSwitch()
				 {
					 Name = "ZTE",
					 Price = 250,
					 Weight = 10,
					 Count = 1
				 },
				 new Entities.NetworkSwitch()
				 {
					 Name = "Huawei",
					 Price = 150,
					 Weight = 15,
					 Count = 1
				 });

			context.Storages.AddIfNameNotExists(
			   new Entities.Storage()
			   {
				   Name = "EMC",
				   Price = 300,
				   Size = 6,
				   Weight = 40
			   },
				new Entities.Storage()
				{
					Name = "IBM",
					Price = 550,
					Size = 6,
					Weight = 50
				},
				new Entities.Storage()
				{
					Name = "HP",
					Price = 750,
					Size = 6,
					Weight = 60
				});

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
		}
	}
}
