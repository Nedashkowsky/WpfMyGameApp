﻿using System;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	public class NetworkSwitch : Entity
	{
		/// <summary>
		/// Кол-во в юнитах
		/// </summary>
		[XmlAttribute()]
		public int Count { get; set; }

		/// <summary>
		/// Вес в кг
		/// </summary>
		[XmlAttribute()]
		public int Weight { get; set; }

		/// <summary>
		/// Адрес картинки
		/// </summary>
		private readonly string img = "Resources/switch.jpeg";

		/// <summary>
		/// Картинка
		/// </summary>
		public string Image
		{
			get { return img; }
		}
	}
}
