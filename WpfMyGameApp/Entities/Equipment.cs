using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfMyGameApp.Entities
{
	public class Equipment : Entity
	{
		/// <summary>
		/// Масса в кг
		/// </summary>
		[XmlAttribute()]
		public int Weight { get; set; }
	}
}
