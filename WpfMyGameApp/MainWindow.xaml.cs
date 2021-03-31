using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfMyGameApp.Entities;

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
		private GameState state;

		public MainWindow()
		{

			InitializeComponent();
		}

		private void CreateCard()
		{
			var server = new Server()
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

			var kvm = new KvmConsole()
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

			var rack = new Rack()
			{
				Name = "Rack",
				Price = 900,
				Capacity = 100,
				Count = 2
			};

			var card3 = new CardControl()
			{
				DataContext = rack
			};
			Canvas.SetLeft(card3, 500);
			Canvas.SetTop(card3, 320);
			canvas.Children.Add(card3);

			var nwswitch = new NetworkSwitch()
			{
				Name = "Switch",
				Price = 350,
				Weight = 2,
				Count = 6
			};

			var card4 = new CardControl()
			{
				DataContext = nwswitch
			};
			Canvas.SetLeft(card4, 700);
			Canvas.SetTop(card4, 80);
			canvas.Children.Add(card4);

			var storage = new Storage()
			{
				Name = "Storage",
				Price = 700,
				Weight = 10,
				Size = 200
			};

			var card5 = new CardControl()
			{
				DataContext = storage
			};
			Canvas.SetLeft(card5, 700);
			Canvas.SetTop(card5, 200);
			canvas.Children.Add(card5);
		}

		/// <summary>
		/// Загрузка окна
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Формирование шкафа
			for (var i = 0; i < 8; i++)
			{
				var item = new CardControl()
				{
					AllowDrop = true
				};

				Canvas.SetLeft(item, 30);
				Canvas.SetTop(item, 30 + i * (height + spanY));
				item.Drop += CardControl.card_Drop;

				// Сохранение ячейки шкафа в список
				cells.Add(item);

				canvas.Children.Add(item);
			}
			cells[7].IsRackCard = true;
			CreateCard();
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

		string json;

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
					state = new GameState();
					foreach (CardControl cell in cells)
					{
						if (cell.DataContext is Server)
							state.Entities.Add(cell.DataContext as Server);
						else if (cell.DataContext is KvmConsole)
							state.Entities.Add(cell.DataContext as KvmConsole);
						else if (cell.DataContext is Rack)
							state.Entities.Add(cell.DataContext as Rack);
						else if (cell.DataContext is NetworkSwitch)
							state.Entities.Add(cell.DataContext as NetworkSwitch);
						else if (cell.DataContext is Storage)
							state.Entities.Add(cell.DataContext as Storage);
						else state.Entities.Add(new Server());
					}

					switch (dialog.FilterIndex)
					{
						case 1:
							// Сериализация в XML
							state.Save(dialog.FileName);
							break;
						case 2:
							// Сериализация в JSON
							json = Newtonsoft.Json.JsonConvert.SerializeObject(state);
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
					Filter = "Файл (*.xml)|*.xml|Файл (*.json)|*.json|Все файлы (*.*)|*.*"
				};
				// Выбор файла для загрузки
				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					state = GameState.Load(dialog.FileName);

					switch (dialog.FilterIndex)
					{
						// XML
						case 1:
							for (int i = 0; i < 8; i++)
							{
								cells[i].DataContext = state.Entities[i];
							}
							break;
						// JSON
						case 2:
							// с десериализацией пока проблемы
							//state = Newtonsoft.Json.JsonConvert.DeserializeObject<GameState>(dialog.FileName);
							//for (int i = 0; i < 8; i++)
							//{
							//	cells[i].DataContext = state.Entities[i];
							//}
							break;
						default:
							break;
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
