using System.Drawing;
using System.Timers;

using RepositoriesImplementations;
using Logic;
using Model;

using User_Interface;

using Timer = System.Timers.Timer;

namespace CLI
{
	public class AlarmClockCLI: IAlarmClockUI
	{
		IAlarmClockService _alarmClockService;
		Timer _checkForTime = new(60 * 1000);

		public AlarmClockCLI()
		{
			_alarmClockService = new AlarmClockService(new AlarmClockFileRepo());
			_checkForTime.Enabled = true;
		}

		public void CreateAlarmClock()
		{
			Console.WriteLine("Создание нового будильника. " +
				"Введите время будильника: ");
			DateTime alarmTime = DateTime.Parse(Console.ReadLine());
			Console.WriteLine("Введите название будильника: ");
			string alarmName = Console.ReadLine();
			Console.WriteLine("Введите цвет будильника на английском, которым он будет подавать сигналы: ");
			Color alarmColor = Color.FromName(Console.ReadLine());
			Console.WriteLine("Запустить будильник сейчас (y/n): ");
			bool isWorking = Console.ReadLine() == "y";

			try
			{
				_alarmClockService.Create(new AlarmClock(alarmTime, alarmName, alarmColor, isWorking));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Не удалось создать будильник. {ex.Message}.");
				return;
			}

			Console.WriteLine("Будильник создан.\n\n" +
				$"Время (дд/ММ/гггг ЧЧ-мм-сс): {alarmTime}\n" +
				$"Название: {alarmName}\n" +
				$"Цвет: {alarmColor}\n" +
				$"Режим: {(isWorking == true ? "включён" : "выключен")}\n"
			);
		}

		public void DeleteAlarmClock()
		{
			ShowAlarmClocks();
			Console.WriteLine("============\n" + "Выберите время, по которому будильник будет удалён (дд/ММ/гггг ЧЧ-мм-сс):");

			DateTime dateTime = DateTime.Parse(Console.ReadLine());
			bool flag = false;
			foreach (var alarmClock in _alarmClockService.GetAllAlarmClocks())
				if (dateTime == alarmClock.AlarmTime)
				{
					try
					{
						_alarmClockService.Delete(dateTime);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Не удалось удалить будильник. {ex.Message}.");
						return;
					}

					flag = true;
				}

			if (flag == false)
				Console.WriteLine("Таких будильников не найдено.");
			else
				Console.WriteLine("Будильник удалён.");
		}

		public void EditAlarmClock()
		{
			ShowAlarmClocks();
			Console.Write("============\n" + "Выберите время, по которому будет изменён будильник (дд/ММ/гггг ЧЧ-мм-сс): ");
			DateTime dateTime = DateTime.Parse(Console.ReadLine());

			bool flag = false;
			foreach (var alarmClock in _alarmClockService.GetAllAlarmClocks())
				if (dateTime == alarmClock.AlarmTime)
				{
					ChooseAlarmClockParam(alarmClock);
					try
					{
						_alarmClockService.Edit(alarmClock, dateTime);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Не удалось изменить будильник. {ex.Message}.");
						return;
					}
					flag = true;
				}

			if (flag == false)
				Console.WriteLine("Таких будильников не найдено.");
			else
				Console.WriteLine("Будильник изменён.");
		}

		private void ChooseAlarmClockParam(AlarmClock alarmClock)
		{
			int choice;
			do
			{
				Console.WriteLine("============\n" +
					"Выберите параметр, по которому будет изменён выбранный будильник:\n" +
					"0 - Готово\n" +
					"1 - Время (дд/ММ/гггг ЧЧ-мм-сс)\n" +
					"2 - Название\n" +
					"3 - Цвет (на английском)\n" +
					$"4 - {(alarmClock.IsWorking == true ? "Выключить" : "Включить")}"
				);
				try
				{
					choice = int.Parse(Console.ReadLine());
				}
				catch
				{
					choice = 0;
				}

				switch (choice)
				{
					case 1:
						Console.Write("Введите новое время для будильника (дд/ММ/гггг ЧЧ-мм-сс): ");
						alarmClock.AlarmTime = DateTime.Parse(Console.ReadLine());
						break;
					case 2:
						Console.Write("Введите новое название будильника: ");
						alarmClock.Name = Console.ReadLine();
						break;
					case 3:
						Console.Write("Введите новый цвет будильника: ");
						alarmClock.AlarmClockColor = Color.FromName(Console.ReadLine());
						break;
					case 4:
						alarmClock.IsWorking = !alarmClock.IsWorking;
						break;
				}
			} while (choice != 0);
		}

		public void Menu()
		{
			_checkForTime.Elapsed += new ElapsedEventHandler(AlarmClockSignal);
			Console.WriteLine("\n0 - Выход из программы.\n" +
				"1 - Показать все будильники.\n" +
				"2 - Установить новый будильник.\n" +
				"3 - Изменить существующий будильник.\n" +
				"4 - Удалить существующий будильник.\n" +
				"\nВведите номер функции из списка:"
			);

			bool flag = false;
			int choice = -1;
			while (flag == false)
			{
				choice = int.Parse(Console.ReadLine());
				if (choice >= 0 && choice <= 4)
				{
					flag = true;
					break;
				}
				else
				{
					flag = false;
					Console.WriteLine("Ошибка ввода. Введите номер функции из списка.");
				}
			}

			switch (choice)
			{
				case 0:
					Exit();
					break;
				case 1:
					ShowAlarmClocks();
					Menu();
					break;
				case 2:
					CreateAlarmClock();
					Menu();
					break;
				case 3:
					EditAlarmClock();
					Menu();
					break;
				case 4:
					DeleteAlarmClock();
					Menu();
					break;
			}
		}

		private ConsoleColor FromColor(Color c)
		{
			int index = c.R > 128 | c.G > 128 | c.B > 128 ? 8 : 0; // Bright bit
			index |= c.R > 64 ? 4 : 0; // Red bit
			index |= c.G > 64 ? 2 : 0; // Green bit
			index |= c.B > 64 ? 1 : 0; // Blue bit
			return (ConsoleColor) index;
		}

		public void AlarmClockSignal(object sender, ElapsedEventArgs e)
		{
			foreach (var alarmClock in _alarmClockService.GetAllAlarmClocks())
				if (DateTime.Now >= alarmClock.AlarmTime)
					for (int i = 0; i < 10; ++i)
					{
						Console.BackgroundColor = FromColor(alarmClock.AlarmClockColor);
						Console.BackgroundColor = ConsoleColor.Black;
					}
		}

		public void ShowAlarmClocks()
		{
			Console.WriteLine("Список всех будильников\n" +
				"============"
			);
			try
			{
				foreach (var alarmclock in _alarmClockService.GetAllAlarmClocks())
					Console.WriteLine($"\nВремя: {alarmclock.AlarmTime}\n" +
						$"Название: {alarmclock.Name}\n" +
						$"Цвет: {alarmclock.AlarmClockColor.Name}\n" +
						$"Режим: {(alarmclock.IsWorking == true ? "включён" : "выключен")}"
					);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Не удалось просмотреть будильник. {ex.Message}.");
				return;
			}

			Console.WriteLine("============");
		}

		public void Exit()
		{
			Console.WriteLine("Выход из приложения...");
			Environment.Exit(0);
		}
	}
}
