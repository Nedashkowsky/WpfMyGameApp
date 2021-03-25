using System;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	/// <summary>
	/// Корневой класс для иерархии
	/// </summary>
	[XmlInclude (typeof(Server))]
	[XmlInclude(typeof(KvmConsole))]
	[XmlInclude(typeof(Rack))]
	[XmlInclude(typeof(Storage))]
	[XmlInclude(typeof(NetworkSwitch))]
	public abstract class Entity
	{
		/// <summary>
		/// Имя
		/// </summary>
		[XmlAttribute()]
		public string Name { get; set; }

		/// <summary>
		/// Стоимость в деньгах
		/// </summary>
		[XmlAttribute()]
		private int price;

		/// <summary>
		/// Стоимость в деньгах
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
