using System;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	/// <summary>
	/// Карточка "Сервер"
	/// </summary>
	public class Server : Entity
	{
		/// <summary>
		/// Кол-во процессоров
		/// </summary>
		[XmlAttribute(AttributeName = "CPU")]
		public int CPUs { get; set; }

		/// <summary>
		/// Размер сервера в юнитах
		/// </summary>
		[XmlAttribute()]
		public int Size { get; set; }

		/// <summary>
		/// Кол-во серверов в отсеке
		/// </summary>
		public int Count
		{
			get
			{
				return (Size == 0) ? 0 : 6 / Size;
			}
		}

		/// <summary>
		/// Адрес картинки
		/// </summary>
		private readonly string img = "server.png";

		/// <summary>
		/// Картинка
		/// </summary>
		public string Image
		{
			get { return img; }
		}
	}
}
