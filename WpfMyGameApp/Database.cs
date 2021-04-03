using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WpfMyGameApp
{
	class Database
	{
		private MySqlConnection connection;

		public Database()
		{
			connection = new MySqlConnection("server=localhost;user=root;database=db;");
			connection.Open();
		}

		/// <summary>
		/// Чтение списка серверов
		/// </summary>
		/// <returns></returns>
		public List<Entities.Server> GetServers()
		{
			var list = new List<Entities.Server>();
			var command = connection.CreateCommand();
			command.CommandText = "SELECT `Name`, `Price`, `Size`, `Weight`, `CPUs`, `Storage` FROM servers";
			MySqlDataReader reader = command.ExecuteReader();
			while(reader.Read())
			{
				var server = new Entities.Server()
				{
					Name = reader.GetString("Name"),
					Price = reader.GetInt32("Price"),
					Size = reader.GetInt32("Size"),
					Weight = reader.GetInt32("Weight"),
					CPUs = reader.GetInt32("CPUs"),
					Storage = reader.GetInt32("Storage"),
				};
				list.Add(server);
			}
			return list;
		}
	}
}
