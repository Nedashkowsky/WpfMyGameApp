using System;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	public class KvmConsole : Equipment
	{
		/// <summary>
		/// Кол-во 
		/// </summary>
		public int Count { get; set; }

		/// <summary>
		/// Адрес картинки
		/// </summary>
		private readonly string img = "Resources/kvm.jpeg";

		/// <summary>
		/// Картинка
		/// </summary>
		public string Image
		{
			get { return img; }
		}
	}
}
