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

		private double dx;
		private double dy;

		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Нажатие кнопки мыши
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rect_MouseDown(object sender, MouseButtonEventArgs e)
		{
			// запомнить смещение для последующей прорисовки
			dx = Canvas.GetLeft((UIElement)sender) - e.GetPosition(this).X;
			dy = Canvas.GetTop((UIElement)sender) - e.GetPosition(this).Y;

			// сохран данных для перетаскивания
			var data = new DataObject();
			data.SetData(DataFormats.StringFormat, "test");
			// начало перетаскивая
			DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Copy);
		}

		/// <summary>
		/// Перемещение мыши
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rect_MouseMove(object sender, MouseEventArgs e)
		{
			// Проверка на нажатие левой кнопки
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				// к текущ координате добавляем смещение
				double x = e.GetPosition(this).X + dx;
				double y = e.GetPosition(this).Y + dy;

				Canvas.SetLeft((UIElement)sender, x);
				Canvas.SetTop((UIElement)sender, y);
			}
		}

		/// <summary>
		/// Завершение перетаскивания
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void slot_Drop(object sender, DragEventArgs e)
		{
			// Принятые данные
			IDataObject data = e.Data;
			/* as string вернет null если не получится привести типы 
			(string) выбросило бы исключение */
			string s = data.GetData(DataFormats.StringFormat) as string; // приведение типа
			// Сетка - приёмник данных
			var grid = sender as Grid;
			var rect = grid.Children[0] as Rectangle;
			rect.Fill = new SolidColorBrush(Colors.LightCyan);
			//slot.Fill = new SolidColorBrush(Colors.Red);
			var text = grid.Children[1] as TextBlock;
			text.Text = s;
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
				var grid = new Grid()
				{
					AllowDrop = true
				};
				Canvas.SetLeft(grid, 30);
				Canvas.SetTop(grid, 30 + i * (height + spanY));
				grid.Drop += slot_Drop;

				// Инициализатор
				var rect = new Rectangle()
				{
					Width = 152,
					Height = height,
					Stroke = new SolidColorBrush(Colors.Black),
					Fill = new SolidColorBrush(Colors.LightGray)
				};
				grid.Children.Add(rect);

				var text = new TextBlock()
				{
					HorizontalAlignment = HorizontalAlignment.Center,
					VerticalAlignment = VerticalAlignment.Center,
					Text = "text"
				};
				grid.Children.Add(text);

				canvas.Children.Add(grid);
			}

			var card = new CardControl();
			Canvas.SetLeft(card, 500);
			Canvas.SetTop(card, 80);
			canvas.Children.Add(card);
		}
	}
}
