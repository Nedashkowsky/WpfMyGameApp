using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfMyGameApp.Entities;
using System.Linq;

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
		public static readonly List<CardControl> cells;

		/// <summary>
		/// Текущее состояние игры
		/// </summary>
		private GameState state;
		
		static MainWindow()
		{
			cells = new List<CardControl>();
		}

		public MainWindow()
		{
			InitializeComponent();
		}

		private void CreateCard()
		{
			var db = new Database();

			// Соединение с новой БД
			var ef = new DB();
			// Запрос списка шкафов
			//var list = ef.Racks.ToList();

			int y = 80;
			foreach(var server in ef.Servers.ToList())
			{
				var card = new CardControl()
				{
					DataContext = server
				};
				Canvas.SetLeft(card, 300);
				Canvas.SetTop(card, y);
				canvas.Children.Add(card);
				y += 100;
			}

			y = 80;
			foreach (var kvm in ef.Kvms.ToList())
			{
				var card = new CardControl()
				{
					DataContext = kvm
				};
				Canvas.SetLeft(card, 500);
				Canvas.SetTop(card, y);
				canvas.Children.Add(card);
				y += 100;
			}

			y = 80;
			foreach (var nwswitch in ef.Switches.ToList())
			{
				var card = new CardControl()
				{
					DataContext = nwswitch
				};
				Canvas.SetLeft(card, 700);
				Canvas.SetTop(card, y);
				canvas.Children.Add(card);
				y += 100;
			}

			y = 80;
			foreach (var storage in ef.Storages.ToList())
			{
				var card = new CardControl()
				{
					DataContext = storage
				};
				Canvas.SetLeft(card, 900);
				Canvas.SetTop(card, y);
				canvas.Children.Add(card);
				y += 100;
			}

			y = 80;
			foreach (var rack in ef.Racks.ToList())
			{
				var card = new CardControl()
				{
					DataContext = rack
				};
				Canvas.SetLeft(card, 1100);
				Canvas.SetTop(card, y);
				canvas.Children.Add(card);
				y += 100;
			}
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
						if (cell.DataContext != null)
						{
							var entity = cell.DataContext as Entity;
							state.Entities.Add(entity);
							// Расчета цены
							state.Price += entity.Price;
							// Расчет нагрузки
							if(entity is Rack)
							{
								state.Capacity += (entity as Rack).Capacity;
							}
							else if (entity is Equipment)
							{
								state.Capacity -= (entity as Equipment).Weight;
							}
							else
							{
								throw new Exception("Неизвестное оборудование в стойке");
							}
						}
						else 
							state.Entities.Add(new Server());
					}

					// Отображение на экране
					price.Text = state.Price.ToString();
					capacity.Text = state.Capacity.ToString();

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
