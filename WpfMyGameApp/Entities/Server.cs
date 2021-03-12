using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	/// <summary>
	/// Карточка "Сервер"
	/// </summary>
	public class Server
	{
		/// <summary>
		/// Имя сервера
		/// </summary>
		[XmlAttribute()]
		public string Name { get; set; }

		/// <summary>
		/// Вес сервера в кг
		/// </summary>
		[XmlAttribute()]
		public int Weight { get; set; }

		/// <summary>
		/// Кол-во процессоров
		/// </summary>
		[XmlAttribute(AttributeName = "CPU")]
		public int CPUs { get; set; }

		/// <summary>
		/// Стоимость сервера в деньгах
		/// </summary>
		[XmlAttribute()]
		private int price;

		/// <summary>
		/// Стоимость сервера в деньгах
		/// </summary>
		[XmlAttribute()]
		public int Price
		{
			get
			{
				return price;
			}
			set
			{
				if (value < 0)
				{
					throw new Exception("Цена не может быть отрицательной");
				}
				price = value;
			}
		}

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
	}
}
