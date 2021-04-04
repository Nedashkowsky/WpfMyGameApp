using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	public class Storage : Equipment
	{
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
		[NotMapped()]
		public string Image
		{
			get { return img; }
		}
	}
}
