using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	/// <summary>
	/// Состояние игры
	/// </summary>
	[XmlInclude(typeof(Server))]
	public class GameState
	{
		/// <summary>
		/// Список верверов
		/// </summary>
		[XmlElement(ElementName = "Server")]
		public Entity[] Servers { get; set; }

		/// <summary>
		/// Конструктор иниациализирующий массив
		/// </summary>
		public GameState()
		{
			Servers = new Server[7];
		}

		/// <summary>
		/// Сериализация в виде XML-файла
		/// </summary>
		/// <param name="name"></param>
		public void Save(string name)
		{
			// Объект для выполнения сериализации
			var ser = new XmlSerializer(GetType());
			// Настройки записи XML-файла
			var settings = new XmlWriterSettings()
			{
				Indent = true
			};
			// Писатель в Xml-файл
			using (XmlWriter wrt = XmlWriter.Create(name,settings))
			{
				// Выполнение сериализации
				ser.Serialize(wrt, this);
			}
		}

		/// <summary>
		/// Десериализация из файла
		/// </summary>
		/// <param name="name">Имя XML-файла</param>
		/// <returns>При ошибке возвращает состояние по умолчанию</returns>
		public static GameState Load(string name)
		{
			try
			{
				var ser = new XmlSerializer(typeof(GameState));
				using (XmlReader rdr = XmlReader.Create(name))
				{
					// Выполнение десериализации
					return (GameState)ser.Deserialize(rdr);
				}
			}
			catch (Exception)
			{
				return new GameState();
			}
		}
	}
}
