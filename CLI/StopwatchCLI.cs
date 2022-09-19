using PPO.Logic;
using PPO.Model;
using PPO.User_Interface;

namespace CLI
{
	public class StopwatchCLI: IStopwatchUI
	{
		private StopwatchService _stopwatchService;

		public StopwatchCLI()
		{
			_stopwatchService = new(new Stopwatch("Секундомер", ))
		}

		public void Exit()
		{
			Console.WriteLine("Выход из приложения...");
			Environment.Exit(0);
		}

		public void Menu()
		{
			Console.WriteLine("Секундомер:\n" +
				$"Название: {}");
			Console.WriteLine("\n0 - Выход из программы.\n" +
				"1 - Запустить секундомер.\n" +
				"2 - Сбросить секундомер.\n" +
				"3 - Остановить секундомер.\n" +
				"4 - Установить флаг.\n" +
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
					ShowNotes();
					Menu();
					break;
				case 2:
					CreateNote();
					Menu();
					break;
				case 3:
					EditNote();
					Menu();
					break;
				case 4:
					DeleteNote();
					Menu();
					break;
			}
		}

		public void ResetStopwatch()
		{
			throw new NotImplementedException();
		}

		public void SetStopwatch()
		{
			throw new NotImplementedException();
		}

		public void SetStopwatchFlag()
		{
			throw new NotImplementedException();
		}

		public void ShowStopwatch()
		{
			throw new NotImplementedException();
		}

		public void StopStopWatch()
		{
			throw new NotImplementedException();
		}
	}
}
