using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMyGameApp.Enities
{
	/// <summary>
	/// Карточка "Сервер"
	/// </summary>
	class Server
	{
		/// <summary>
		/// Имя сервера
		/// </summary>
		public string Name;

		/// <summary>
		/// Вес сервера в кг
		/// </summary>
		public int Weight;

		/// <summary>
		/// Кол-во процессоров
		/// </summary>
		public int CPUs;

		/// <summary>
		/// Стоимость сервера в деньгах
		/// </summary>
		public int Price;

		/// <summary>
		/// Размер сервера в юнитах
		/// </summary>
		public int Units;
	}
}
