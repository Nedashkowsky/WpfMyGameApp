using System;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	/// <summary>
	/// Корневой класс для иерархии
	/// </summary>
	public abstract class Entity
	{
		/// <summary>
		/// Имя
		/// </summary>
		[XmlAttribute()]
		public string Name { get; set; }

		/// <summary>
		/// Вес в кг
		/// </summary>
		[XmlAttribute()]
		public int Weight { get; set; }

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
	}
}
