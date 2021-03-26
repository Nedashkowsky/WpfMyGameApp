using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfMyGameApp.Entities;

namespace WpfMyGameApp
{
	/// <summary>
	/// Логика взаимодействия для CardControl.xaml
	/// </summary>
	public partial class CardControl : UserControl
	{
		private double dx;
		private double dy;

		public string Image
		{
			get
			{
				return img.Source.ToString();
			}
		}

		private bool isRackCard = false;

		/// <summary>
		/// Является ли карточка карточкой для шкафа
		/// </summary>
		public bool IsRackCard
		{
			get { return isRackCard; }

			set 
			{
				isRackCard = value;

				if (isRackCard)
					card.Background = Brushes.Aquamarine;
			}
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
			if(DataContext != null)
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

		/// <summary>
		/// Завершение перетаскивания
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void card_Drop(object sender, DragEventArgs e)
		{
			// Принятые данные
			IDataObject data = e.Data;
			var source = data.GetData("Card");
			// Приёмник данных - карточка
			var dest = sender as CardControl;
			dest.DataContext = source;
		}

		private void CardControl1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			switch(e.NewValue.GetType().Name)
			{
				case nameof(Server):
					var server = e.NewValue as Server;
					card.Background = Brushes.Pink;
					property1.Text = server.Name;
					property2.Text = $"{server.Weight} кг";
					property3.Text = $"{server.Price} USD";
					property4.Text = $"{server.CPUs} CPU";
					property5.Text = $"{server.Size} U";
					property6.Text = $"{server.Count} шт.";
					break;

				case nameof(KvmConsole):
					var kvm = e.NewValue as KvmConsole;
					card.Background = Brushes.Bisque;

					property1.Text = kvm.Name;
					property2.Text = $"{kvm.Count} шт.";
					property3.Text = $"{kvm.Price} USD";
					property4.Text = $"{kvm.Weight} кг"; ;
					break;

				case nameof(Rack):
					var rack = e.NewValue as Rack;
					card.Background = Brushes.Aquamarine;

					property1.Text = rack.Name;
					property2.Text = $"{rack.Count} шт.";
					property3.Text = $"{rack.Price} USD";
					property4.Text = $"{rack.Capacity} кг"; ;
					break;

				case nameof(NetworkSwitch):
					var nwswitch = e.NewValue as NetworkSwitch;
					card.Background = Brushes.Coral;

					property1.Text = nwswitch.Name;
					property2.Text = $"{nwswitch.Count} шт.";
					property3.Text = $"{nwswitch.Price} USD";
					property4.Text = $"{nwswitch.Weight} кг"; ;
					break;

				case nameof(Storage):
					var storage = e.NewValue as Storage;
					card.Background = Brushes.DarkOrchid;

					property1.Text = storage.Name;
					property2.Text = $"{storage.Size} Тбайт";
					property3.Text = $"{storage.Price} USD";
					property4.Text = $"{storage.Weight} кг"; ;
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// Обработчик события перетаскивания карточки в соотвествии с ее типом
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void card_DragOver(object sender, DragEventArgs e)
		{
			e.Effects = DragDropEffects.Copy;
			// Перетаскиваемый объект
			IDataObject data = e.Data;
			object card = data.GetData("Card");

			if (IsRackCard && !(card is Rack))
				e.Effects = DragDropEffects.None;
			else if (!IsRackCard && (card is Rack))
				e.Effects = DragDropEffects.None;
		}
	}
}
