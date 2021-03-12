using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfMyGameApp
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Высота ячейки
		/// </summary>
		private const double height = 60;
		/// <summary>
		/// Поле между ячейками
		/// </summary>
		private const double spanY = 20;

		public MainWindow()
		{

			InitializeComponent();
		}

		/// <summary>
		/// Завершение перетаскивания
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void card_Drop(object sender, DragEventArgs e)
		{
			// Принятые данные
			IDataObject data = e.Data;
			/* as <type> вернет null если не получится привести типы 
			(<type>) выбросило бы исключение */
			var source = data.GetData("Card") as Entities.Server; // приведение типа
			// Приёмник данных - карточка
			var dest = sender as CardControl;
			dest.DataContext = source;
		}

		/// <summary>
		/// Загрузка окна
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Формирование шкафа
			for (var i = 0; i < 7; i++)
			{
				var item = new CardControl()
				{
					AllowDrop = true
				};
				
				Canvas.SetLeft(item, 30);
				Canvas.SetTop(item, 30 + i * (height + spanY));
				item.Drop += card_Drop;

				canvas.Children.Add(item);
			}

			var server = new Entities.Server()
			{
				Name = "IBM",
				Price = 1000,
				Weight = 10,
				CPUs = 2,
				Size = 1
			};

			var card = new CardControl()
			{
				DataContext = server
			};
			Canvas.SetLeft(card, 500);
			Canvas.SetTop(card, 80);
			canvas.Children.Add(card);
		}

		private void exit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Сохранение состояния игры в файл
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void save_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new System.Windows.Forms.SaveFileDialog();
			if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{ 
			//...
			}
		}
	}
}
