namespace CLI
{
	public class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("0 - Выход.\n" +
				"1 - Будильники.\n" +
				"2 - Заметки.\n" +
				"3 - Секундомер.\n"
			);

			bool flag = false;
			int choice = -1;
			while (flag == false)
			{
				choice = int.Parse(Console.ReadLine());
				if (choice >= 0 && choice <= 3)
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
					Console.WriteLine("Выход из приложения...");
					Environment.Exit(0);
					break;
				case 1:
					AlarmClockCLI alarmClockCLI = new();
					alarmClockCLI.Menu();
					break;
				case 2:
					NoteCLI noteCLI = new();
					noteCLI.Menu();
					break;
				case 3:
					StopwatchCLI stopwatchCLI = new();
					stopwatchCLI.Menu();
					break;
			}
		}
	}
}
