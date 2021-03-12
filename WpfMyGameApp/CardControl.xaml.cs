using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMyGameApp
{
	/// <summary>
	/// Логика взаимодействия для CardControl.xaml
	/// </summary>
	public partial class CardControl : UserControl
	{
		private double dx;
		private double dy;

		/// <summary>
		/// Имя сервера
		/// </summary>
		public string Server
		{
			get { return name.Text; }
			set { name.Text = value; }
		}

		/// <summary>
		/// Цена сервера
		/// </summary>
		public int Price
		{
			get 
			{
				int result;
				return int.TryParse(price.Text, out result) ? result : -1;
			}
			set { price.Text = value.ToString(); }
		}

		public CardControl()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Нажатие кнопки мыши
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			// Запомнить смещение для последующей прорисовки
			dx = Canvas.GetLeft((UIElement)sender) - e.GetPosition(this).X;
			dy = Canvas.GetTop((UIElement)sender) - e.GetPosition(this).Y;

			// Сохранение данных для перетаскивания
			var data = new DataObject();
			// Сохранение контекста данных
			data.SetData("Card", DataContext);
			// Начало перетаскивая
			DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Copy);
		}

		/// <summary>
		/// Перемещение мыши
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void grid_MouseMove(object sender, MouseEventArgs e)
		{
			// Проверка на нажатие левой кнопки
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				// К текущей координате добавляем смещение
				double x = e.GetPosition(this).X + dx;
				double y = e.GetPosition(this).Y + dy;

				Canvas.SetLeft((UIElement)sender, x);
				Canvas.SetTop((UIElement)sender, y);
			}
		}
	}
}
