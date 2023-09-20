using Logic;

using RepositoriesImplementations;

using System.Drawing;
using System.Timers;

using UserInterface;

using Notification = Model.Notification;
using Timer = System.Timers.Timer;

namespace CLI
{
	public class NotificationCLI: INotificationUI
	{
		private INotificationService _notificationService;
		Timer _checkForTime = new(7000);

		public NotificationCLI()
		{
			_notificationService = new NotificationService(new NotificationFileRepo());
			_checkForTime.Enabled = true;
		}

		public void CreateNotification()
		{
			Console.Write("Создание нового напоминания. Введите время напоминания (дд/ММ/гггг ЧЧ:мм:сс): ");
			DateTime notifyTime;
			while (!DateTime.TryParse(Console.ReadLine(), out notifyTime))
				Console.Write("Ошибка ввода. Введите время напоминания: ");

			Console.WriteLine("Введите содержимое напоминания. Нажмите Esc для сохранения текста:");
			string notificationBody = "";
			ConsoleKeyInfo key = new();
			while (key.Key != ConsoleKey.Escape) {
				key = Console.ReadKey();
				notificationBody += key.KeyChar;
			}

			Console.WriteLine("Введите цвет напоминания на английском, которым оно будет подавать сигналы: ");
			Color notificationColor = Color.FromName(Console.ReadLine());

			Console.Write("\nВключить напоминание (y/n): ");
			bool isWorking = Console.ReadLine() == "y";

			Notification notification = new(
				Guid.NewGuid(),
				notifyTime,
				notificationBody,
				notificationColor,
				isWorking);

			try {
				_notificationService.Create(notification);
			} catch (Exception ex) {
				Console.WriteLine($"Не удалось создать напоминание. {ex.Message}.");
				return;
			}

			Console.WriteLine(
				"\nНапоминание сохранено.\n\n" +
				$"Уникальный идентификатор: {notification.Id}" +
				$"Время напоминания: {notification.NotifyTime}\n" +
				$"Текст напоминания\n: {notification.Body}\n" +
				$"Цвет напоминания: {notification.NotificationColor.Name}\n" +
				$"Режим: {(notification.IsWorking == true ? "да" : "нет")}\n"
			);
		}

		public void DeleteNotification()
		{
			ShowNotifications();
			Console.Write("============\nВыберите идентификатор, по которому будет удалено напоминание: ");
			Guid guid;
			while (!Guid.TryParse(Console.ReadLine(), out guid))
				Console.Write("Ошибка ввода. Введите идентификатор напоминания: ");

			bool flag = false;
			foreach (var notification in _notificationService.GetAllNotificationsList())
				if (guid == notification.Id) {
					try {
						_notificationService.Delete(guid);
					} catch (Exception ex) {
						Console.WriteLine($"Не удалось удалить напоминание. {ex.Message}.");
						return;
					}

					flag = true;
				}

			if (flag == false)
				Console.WriteLine("Таких напоминаний не найдено.");
			else
				Console.WriteLine("Напоминание удалена.");
		}

		public void EditNotification()
		{
			ShowNotifications();
			Console.Write("============\nВыберите идентификатор, по которому будет изменено напоминание: ");
			Guid guid;
			while (!Guid.TryParse(Console.ReadLine(), out guid))
				Console.Write("Ошибка ввода. Введите идентификатор напоминания: ");

			bool flag = false;
			foreach (var notification in _notificationService.GetAllNotificationsList())
				if (guid == notification.Id) {
					ChooseNoteParam(notification);
					try {
						_notificationService.Edit(notification);
					} catch (Exception ex) {
						Console.WriteLine($"Не удалось изменить напоминание. {ex.Message}.");
						return;
					}
					flag = true;
				}

			if (flag == false)
				Console.WriteLine("Таких напоминаний не найдено.");
			else
				Console.WriteLine("Напоминание изменена.");
		}

		private void ChooseNoteParam(Notification notification)
		{
			int choice;
			do {
				Console.WriteLine(
					"\n============\n" +
					"Выберите параметр, по которому будет изменено выбранное напоминание:\n" +
					"0 - Готово\n" +
					"1 - Текст\n" +
					"2 - Время напоминания\n" +
					$"3 - {(notification.IsWorking == true ? "Выключить" : "Включить")}"
				);
				try {
					choice = int.Parse(Console.ReadLine());
				} catch {
					choice = 0;
				}

				switch (choice) {
					case 1:
						Console.WriteLine($"Старый текст напоминания:\n {notification.Body}");
						Console.WriteLine("Новый текст напоминания (нажмите Esc для сохранения текста):");
						notification.Body = "";

						ConsoleKeyInfo key = new();
						while (key.Key != ConsoleKey.Escape) {
							key = Console.ReadKey();
							notification.Body += key.KeyChar;
						}
						break;
					case 2:
						Console.Write("Введите время напоминания: ");
						DateTime dateTime;
						while (!DateTime.TryParse(Console.ReadLine(), out dateTime))
							Console.Write("Ошибка ввода. Введите время напоминания (дд/ММ/гггг ЧЧ:мм:сс): ");

						notification.NotifyTime = dateTime;
						break;
					case 3:
						notification.IsWorking = !notification.IsWorking;
						Console.WriteLine($"Напоминание {(notification.IsWorking == true ? "включено" : "выключено")}");
						break;
					default:
						continue;
				}
			} while (choice != 0);
		}

		public void Exit()
		{
			Console.WriteLine("Выход из приложения...");
			Environment.Exit(0);
		}

		public void Menu()
		{
			_checkForTime.Elapsed += NotificationSignalAsync;
			Console.WriteLine(
				"\n0 - Выход из программы.\n" +
				"1 - Показать все напоминания.\n" +
				"2 - Создать новое напоминание.\n" +
				"3 - Редактировать напоминание.\n" +
				"4 - Удалить напоминание.\n" +
				"\nВведите номер функции из списка:"
			);

			bool flag = false;
			int choice = -1;
			while (flag == false) {
				try {
					choice = int.Parse(Console.ReadLine());
				} catch {
					choice = -1;
				}

				if (choice >= 0 && choice <= 4)
					flag = true;
				else {
					flag = false;
					Console.WriteLine("Ошибка ввода. Введите номер функции из списка.");
				}
			}

			switch (choice) {
				case 0:
					Exit();
					break;
				case 1:
					ShowNotifications();
					Menu();
					break;
				case 2:
					CreateNotification();
					Menu();
					break;
				case 3:
					EditNotification();
					Menu();
					break;
				case 4:
					DeleteNotification();
					Menu();
					break;
			}
		}

		public void ShowNotifications()
		{
			Console.WriteLine("Список всех напоминаний\n============");
			try {
				foreach (var notification in _notificationService.GetAllNotificationsList())
					Console.WriteLine(
						$"Уникальный идентификатор: {notification.Id}\n" +
						$"Время напоминания: {notification.NotifyTime}\n" +
						$"Текст напоминания:\n{notification.Body}\n" +
						$"Цвет напоминания: {notification.NotificationColor.Name}\n" +
						$"Режим: {(notification.IsWorking == true ? "включено" : "выключено")}\n"
					);
			} catch (Exception ex) {
				Console.WriteLine($"Не удалось просмотреть напоминание. {ex.Message}.");
				return;
			}
			Console.WriteLine("============");
		}

		private ConsoleColor FromColor(Color c)
		{
			int index = c.R > 128 | c.G > 128 | c.B > 128 ? 8 : 0;
			index |= c.R > 64 ? 4 : 0;
			index |= c.G > 64 ? 2 : 0;
			index |= c.B > 64 ? 1 : 0;
			return (ConsoleColor) index;
		}

		public void NotificationSignalAsync(object sender, ElapsedEventArgs e)
		{
			foreach (var notification in _notificationService.GetAllNotificationsList())
				if (notification.IsWorking &&
					DateTime.Now >= notification.NotifyTime &&
					DateTime.Now <= notification.NotifyTime + new TimeSpan(0, 0, 30)
				) {
					for (int i = 0; i < 5; ++i) {
						//Console.Clear();
						//Console.BackgroundColor = FromColor(notification.NotificationColor);
						Console.ForegroundColor = FromColor(notification.NotificationColor);
						Console.WriteLine(notification.Body);
						Thread.Sleep(500);
						//Console.Clear();
						Console.ResetColor();
					}
					Console.Clear();

					Console.WriteLine($"Только что сработало напоминание, установленное на время {notification.NotifyTime}. Теперь это напоминание выключено.");

					notification.IsWorking = false;
					_notificationService.Edit(notification);
					Menu();
				}
		}
	}
}
