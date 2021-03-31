using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace WpfMyGameApp.Entities
{
	/// <summary>
	/// Состояние игры
	/// </summary>
	[XmlRoot(ElementName = "State")]
	public class GameState
	{
		/// <summary>
		/// Список сущностей
		/// </summary>
		[XmlElement(ElementName = "Entity")]
		public List<Entity> Entities { get; set; }

		/// <summary>
		/// Суммарная цена стойки
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// Оставшаяся нагрузочная способность стойки в кг
		/// <para>Вес не дложен быть меньше нуля</para>
		/// </summary>
		public int Capacity { get; set; }

		/// <summary>
		/// Конструктор без параметров
		/// </summary>
		public GameState()
		{
			Entities = new List<Entity>();
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
