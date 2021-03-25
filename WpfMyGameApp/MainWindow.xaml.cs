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

		/// <summary>
		/// Список ячеек шкафа
		/// </summary>
		private List<CardControl> cells = new List<CardControl>();

		/// <summary>
		/// Текущее состояние игры
		/// </summary>
		private Entities.GameState state;

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
			var source = data.GetData("Card"); 
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

				// Сохранение ячейки шкафа в список
				cells.Add(item);

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

			var kvm = new Entities.KvmControl()
			{
				Name = "ATEL",
				Price = 300,
				Weight = 5,
				Count = 3
			};

			var card2 = new CardControl()
			{
				DataContext = kvm
			};
			Canvas.SetLeft(card2, 500);
			Canvas.SetTop(card2, 200);
			canvas.Children.Add(card2);
		}

		/// <summary>
		/// Выход из приложения
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
			try
			{
				// Диалог для выбора имени файла
				var dialog = new System.Windows.Forms.SaveFileDialog()
				{
					Filter = "Файл (*.xml)|*.xml|Файл (*.json)|*.json|Все файлы (*.*)|*.*"
				};

				// Выбор файла для сохранения
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					List<Entities.Server> servers = new List<Entities.Server>();
					foreach (CardControl cell in cells)
					{
						if (cell.DataContext is Entities.Server)
							servers.Add(cell.DataContext as Entities.Server);
						else
							servers.Add(new Entities.Server());
					}

					state = new Entities.GameState()
					{
						Servers = servers.ToArray()
					};
					switch (dialog.FilterIndex)
					{
						case 1:
							// Сериализация в XML
							state.Save(dialog.FileName);
							break;
						case 2:
							// Сериализация в JSON
							string json = Newtonsoft.Json.JsonConvert.SerializeObject(state);
							System.IO.File.WriteAllText(dialog.FileName, json);
							break;
						default:
							break;
					}

				}
			}
			catch (Exception ex)
			{
				string message = "";
				do
				{
					message += ex.Message + Environment.NewLine;
					ex = ex.InnerException;
				}
				while (ex != null);
				System.Windows.Forms.MessageBox.Show(message);
			}
		}

		/// <summary>
		/// Загрузка игры из файла
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void load_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				// Диалог для выбора имени файла
				var dialog = new System.Windows.Forms.OpenFileDialog()
				{
					Filter = "Файл (*.xml)|*.xml|Все файлы (*.*)|*.*"
				};
				// Выбор файла для загрузки
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					state = Entities.GameState.Load(dialog.FileName);
					for (int i = 0; i < 7; i++)
					{
						cells[i].DataContext = state.Servers[i];
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}
		}
	}
}
