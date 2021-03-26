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

		/// <summary>
		/// Адрес картинки
		/// </summary>
		private readonly string img = "Resources/storage.png";

		/// <summary>
		/// Картинка
		/// </summary>
		public string Image
		{
			get { return img; }
		}
	}
}
