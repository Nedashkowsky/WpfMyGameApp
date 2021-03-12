using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMyGameApp.Entities
{
	/// <summary>
	/// Карточка "Сервер"
	/// </summary>
	class Server
	{
		/// <summary>
		/// Имя сервера
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Вес сервера в кг
		/// </summary>
		public int Weight { get; set; }

		/// <summary>
		/// Кол-во процессоров
		/// </summary>
		public int CPUs { get; set; }

		/// <summary>
		/// Стоимость сервера в деньгах
		/// </summary>
		private int price;

		/// <summary>
		/// Стоимость сервера в деньгах
		/// </summary>
		public int Price 
		{
			get
			{
				return price;
			}
			set
			{
				if(value < 0)
				{
					throw new Exception("Цена не может быть отрицательной");
				}
				price = value;
			}
		}

		/// <summary>
		/// Размер сервера в юнитах
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Кол-во серверов в отсеке
		/// </summary>
		public int Count 
		{ 
			get { return 6 / Size; } 
		}
	}
}
