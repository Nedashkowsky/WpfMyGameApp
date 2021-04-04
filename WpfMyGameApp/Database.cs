using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
			while (reader.Read())
			{
				var server = new Entities.Server();
				// Последовательная обработка всех столбцов таблицы
				for (int col = 0; col < reader.FieldCount; col++)
				{
					// Имя столбца таблицы (`Price`, `Size` и тд)
					string column = reader.GetName(col);
					// Имя типа данных столбца таблицы
					string type = reader.GetFieldType(col).Name;
					// Свойство объекта server с именем равным имени столбца таблицы
					PropertyInfo prop = server.GetType().GetProperty(column);
					switch (type)
					{
						case nameof(String):
							prop.SetValue(server, reader.GetString(col));
							break;
						case nameof(Int32):
							prop.SetValue(server, reader.GetInt32(col));
							break;
						default:
							throw new Exception($"Тип данных '{type}' не поддерживается");
					}
				}
				list.Add(server);
			}
			return list;
		}
	}
}
