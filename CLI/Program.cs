using Microsoft.Extensions.Configuration;

using Serilog;

namespace CLI
{
	public class Program
	{
		static void Main(string[] args)
		{
			var _config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			Log.Logger = new LoggerConfiguration()
				.WriteTo.File(_config["Logger"])
				.CreateLogger();

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
					Log.Logger.Debug($"{DateTime.Now}: Осуществлён выход из приложения.");
					Environment.Exit(0);
					break;
				case 1:
					AlarmClockCLI alarmClockCLI = new();
					alarmClockCLI.Menu();
					Log.Logger.Debug($"{DateTime.Now}: Выбран пункт будильников.");
					break;
				case 2:
					NoteCLI noteCLI = new();
					noteCLI.Menu();
					Log.Logger.Debug($"{DateTime.Now}: Выбран пункт заметок.");
					break;
				case 3:
					StopwatchCLI stopwatchCLI = new();
					stopwatchCLI.Menu();
					Log.Logger.Debug($"{DateTime.Now}: Выбран пункт секундомера.");
					break;
			}
			Log.CloseAndFlush();
		}
	}
}
