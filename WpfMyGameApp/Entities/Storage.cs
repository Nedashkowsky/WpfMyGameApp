using System;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	public class Storage : Entity
	{
		/// <summary>
		/// Вес в кг
		/// </summary>
		[XmlAttribute()]
		public int Weight { get; set; }

		/// <summary>
		/// Ёмкость Тб
		/// </summary>
		[XmlAttribute()]
		public int Size { get; set; }
	}
}
