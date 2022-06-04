using System;
using System.ComponentModel.DataAnnotations;
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
		/// Первичный ключs
		/// </summary>
		[Key()]	// Не обязательно, т. к. используется имя ID
		public Guid ID { get; set; }

		/// <summary>
		/// Имя
		/// </summary>
		[XmlAttribute()]
		[MaxLength(127)]
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

		public Entity()
		{
			ID = Guid.NewGuid();
		}
	}
}
