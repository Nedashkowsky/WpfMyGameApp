using System;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfMyGameApp.Entities
{
	[Table("Kvm Modules")]
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
