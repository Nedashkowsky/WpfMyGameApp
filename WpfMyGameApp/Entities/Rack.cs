using System;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	public class Rack : Entity
	{
		/// <summary>
		/// Кол-во в юнитах
		/// </summary>
		[XmlAttribute()]
		public int Count { get; set; }

		/// <summary>
		/// Нагрузка в кг
		/// </summary>
		[XmlAttribute()]
		public int Capacity { get; set; }
	}
}
