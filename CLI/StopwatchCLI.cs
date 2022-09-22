using Logic;
using Model;

using System.Drawing;

using User_Interface;

using SysStopwatch = System.Diagnostics;

namespace CLI
{
	public class StopwatchCLI: IStopwatchUI
	{
		private IStopwatchService _stopwatchService;

		public StopwatchCLI()
		{
			_stopwatchService = new StopwatchService(new Stopwatch(
				"Секундомер",
				Color.FromName("Red"),
				new SysStopwatch.Stopwatch(),
				new SortedSet<DateTime>(),
				false
			));
		}

		public void Exit()
		{
			Console.WriteLine("Выход из приложения...");
			Environment.Exit(0);
		}

		public void Menu()
		{
			Console.WriteLine("Секундомер:\n" +
				$"Название: {_stopwatchService.Get().Name}\n" +
				$"Цвет: {_stopwatchService.Get().StopwatchColor}\n" +
				$"Время: {_stopwatchService.Get().Timing.Elapsed}\n" +
				$"Режим работы: {(_stopwatchService.Get().IsWorking == true ? "Запущен" : "Не запущен")}\n");
			Console.WriteLine("Флаги:\n");
			ShowFlags();
			Console.WriteLine("\n0 - Выход из программы.\n" +
				"1 - Запустить секундомер.\n" +
				"2 - Сбросить секундомер.\n" +
				"3 - Остановить секундомер.\n" +
				"4 - Установить флаг.\n" +
				"5 - Поменять цвет.\n" +
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
					SetStopwatch();
					Menu();
					break;
				case 2:
					ResetStopwatch();
					Menu();
					break;
				case 3:
					StopStopWatch();
					Menu();
					break;
				case 4:
					AddStopwatchFlag();
					Menu();
					break;
				case 5:
					EditStopwatchColor();
					Menu();
					break;
			}
		}

		private void ShowFlags()
		{
			foreach (var elem in _stopwatchService.Get().TimeFlags)
				Console.WriteLine(elem);
		}

		private void EditStopwatchColor()
		{
			Console.Write("Введите название цвета на английском: ");
			Color color = Color.FromName(Console.ReadLine());
			_stopwatchService.EditColor(color);
			Console.WriteLine("Цвет изменён.");
		}

		public void ResetStopwatch()
		{
			_stopwatchService.Reset();
			_stopwatchService.Get().TimeFlags.Clear();
			_stopwatchService.Get().IsWorking = false;
			Console.WriteLine("Секундомер сброшен.");
		}

		public void SetStopwatch()
		{
			_stopwatchService.Set();
			_stopwatchService.Get().IsWorking = true;
			Console.WriteLine("Секундомер установлен.");
		}

		public void AddStopwatchFlag()
		{
			Console.WriteLine($"Флаг на: {_stopwatchService.SetFlag()}.");
		}

		public void StopStopWatch()
		{
			_stopwatchService.Get().IsWorking = false;
			Console.WriteLine($"Секундомер остановлен. Время: {_stopwatchService.Stop()}.");
		}
	}
}
