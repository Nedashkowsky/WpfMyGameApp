﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	public class Rack : Entity
	{
		/// <summary>
		/// Кол-во в юнитах
		/// </summary>
		[XmlAttribute()]
		public int Count { get; set; }

		/// <summary>
		/// Нагрузка в кг
		/// </summary>
		[XmlAttribute()]
		public int Capacity { get; set; }

		/// <summary>
		/// Адрес картинки
		/// </summary>
		private readonly string img = "Resources/rack.png";

		/// <summary>
		/// Картинка
		/// </summary>
		[XmlIgnore()]
		[NotMapped()] // не отображается на БД
		public string Image
		{
			get { return img; }
		}
	}
}
