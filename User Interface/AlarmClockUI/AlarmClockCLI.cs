using System;

namespace PPO.User_Interface.AlarmClockUI
{
	public class AlarmClockCLI: IAlarmClockUI
	{
		public void CreatAlarmClock()
		{
			throw new NotImplementedException();
		}

		public void DeleteAlarmClock()
		{
			throw new NotImplementedException();
		}

		public void EditAlarmClock()
		{
			throw new NotImplementedException();
		}

		public void Menu()
		{
			Console.WriteLine("0 - Выход из программы.\n" +
				"1 - Показать все будильники.\n" +
				"2 - Установить новый будильник.\n" +
				"3 - Изменить существующий будильник.\n" +
				"4 - Удалить существующий будильник.\n" +
				"\nВведите номер функции из списка: "
			);

			bool flag = false;
			int choice = -1;
			while (flag == false)
			{
				choice = int.Parse(Console.ReadLine());
				if (choice < 0 || choice > 4)
				{
					flag = true;
					break;
				}
				else
					flag = false;
			}

			switch (choice)
			{
				case 0:
					Exit();
					break;
				case 1:
					ShowAlarmClocks();
					break;
				case 2:
					CreatAlarmClock();
					break;
				case 3:
					EditAlarmClock();
					break;
				case 4:
					DeleteAlarmClock();
					break;
			}
		}

		public void ShowAlarmClocks()
		{
			throw new NotImplementedException();
		}

		public void Exit()
		{
			Console.WriteLine("Выход из приложения...");
			Environment.Exit(0);
		}
	}
}
