using Model;
using System.Drawing;
using System.IO.IsolatedStorage;

using Serilog;
using Serilog.Core;

namespace Data
{
	public class AlarmClockFileRepo: IAlarmClockRepo
	{
		IsolatedStorageFile _isoStore;
		Logger _logger = new LoggerConfiguration()
			.WriteTo.File("LogAlarmClock.txt")
			.CreateLogger();

		public AlarmClockFileRepo()
		{
			_isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
			_isoStore.CreateDirectory("alarmclocks");
			_logger.Error($"{DateTime.Now}: Создана папка для будильников.");
			Log.CloseAndFlush();
		}

		public void Create(AlarmClock alarmClock)
		{
			if (_isoStore.AvailableFreeSpace <= 0)
			{
				_logger.Error($"{DateTime.Now}: Место в защищённом хранилище будильников закончилось.");
				throw new IOException();
			}
			string filepath = $"alarmclocks/{alarmClock.AlarmTime:dd/MM/yyyy HH-mm-ss}.txt";

			using StreamWriter writer = new(_isoStore.CreateFile(filepath));
			writer.WriteLine(alarmClock.Name);
			writer.WriteLine(alarmClock.AlarmClockColor.Name);
			writer.WriteLine(alarmClock.IsWorking);

			_logger.Information($"{DateTime.Now}: Создан файл будильника со следующей информацией:" +
				$"{alarmClock.Name}," +
				$"{alarmClock.AlarmTime}," +
				$"{alarmClock.AlarmClockColor.Name}," +
				$"{alarmClock.IsWorking}."
			);
			Log.CloseAndFlush();
		}

		public void Edit(AlarmClock alarmClock, DateTime oldTime)
		{
			IsolatedStorageFileStream isoStream;
			try
			{
				isoStream = new(
					$"alarmclocks/{oldTime:dd/MM/yyyy HH-mm-ss}.txt",
					FileMode.Create,
					FileAccess.Write,
					_isoStore
				);
			}
			catch
			{
				_logger.Error($"{DateTime.Now}: Файл с названием \"alarmclocks/{oldTime:dd/MM/yyyy HH-mm-ss}.txt\" не найден.");
				throw new IOException();
			}

			using (StreamWriter writer = new(isoStream))
			{
				writer.WriteLine(alarmClock.Name);
				writer.WriteLine(alarmClock.AlarmClockColor.Name);
				writer.WriteLine(alarmClock.IsWorking);
			}

			_logger.Information($"{DateTime.Now}: Изменён файл будильника следующей информацией:" +
				$"{alarmClock.Name}," +
				$"{alarmClock.AlarmTime}," +
				$"{alarmClock.AlarmClockColor.Name}," +
				$"{alarmClock.IsWorking}."
			);

			_isoStore.MoveFile($"alarmclocks/{oldTime:dd/MM/yyyy HH-mm-ss}.txt", $"alarmclocks/{alarmClock.AlarmTime:dd/MM/yyyy HH-mm-ss}.txt");

			_logger.Information($"{DateTime.Now}: Файл будильника переименован.\n " +
				$"Старое название файла: \"alarmclocks/{oldTime:dd/MM/yyyy HH-mm-ss}.txt\".\n" +
				$"Новое название файла: \"alarmclocks/{alarmClock.AlarmTime:dd/MM/yyyy HH-mm-ss}.txt\"."
			);
			Log.CloseAndFlush();
		}

		public void Delete(DateTime alarmTime)
		{
			IsolatedStorageFileStream isoStream;
			string filepath = $"alarmclocks/{alarmTime:dd/MM/yyyy HH-mm-ss}.txt";
			try
			{
				isoStream = new(
					filepath,
					FileMode.Open,
					FileAccess.Write,
					_isoStore
				);
			}
			catch
			{
				_logger.Error($"{DateTime.Now}: Файл с названием \"alarmclocks/{alarmTime:dd/MM/yyyy HH-mm-ss}.txt\" не найден.");
				throw new IOException();
			}

			isoStream.Close();
			_isoStore.DeleteFile(filepath);

			_logger.Information($"{DateTime.Now}: Удалён файл будильника. Время будильника: {alarmTime}.");

			Log.CloseAndFlush();
		}

		public AlarmClock? GetAlarmClock(DateTime alarmTime)
		{
			string[] filelist;
			string filepath = $"alarmclocks/{alarmTime:dd/MM/yyyy HH-mm-ss}.txt";
			try
			{
				filelist = _isoStore.GetFileNames(filepath);
			}
			catch
			{
				_logger.Error($"{DateTime.Now}: Папка для будильников в защищённом хранилище не найдена.");
				throw new IOException();
			}

			foreach (string fileName in filelist)
				if (fileName.Equals($"{alarmTime:dd/MM/yyyy HH-mm-ss}.txt"))
				{
					using var readerStream = new StreamReader(new IsolatedStorageFileStream(
						filepath,
						FileMode.Open,
						FileAccess.Read,
						_isoStore
					));
					string? alarmClockName = readerStream.ReadLine();
					string? alarmClockColor = readerStream.ReadLine();
					string? alarmClockWork = readerStream.ReadLine();
					if (alarmClockName == null || alarmClockColor == null || alarmClockWork == null)
					{
						_logger.Error($"{DateTime.Now}: Ошибка разметки файла будильника. Время будильника: {alarmTime}.");
						throw new ArgumentNullException();
					}

					return new AlarmClock(
						alarmTime,
						alarmClockName,
						Color.FromName(alarmClockColor),
						bool.Parse(alarmClockWork)
					);
				}
			Log.CloseAndFlush();

			return null;
		}

		public List<AlarmClock> GetAlarmClocksList(string pattern)
		{
			string[] filelist;
			try
			{
				filelist = _isoStore.GetFileNames(pattern);
			}
			catch
			{
				_logger.Error($"{DateTime.Now}: Папка для будильников в защищённом хранилище не найдена.");
				throw new IOException();
			}

			List<AlarmClock> alarmClockList = new();
			foreach (string fileName in filelist)
			{
				var alarmClock = GetAlarmClock(DateTime.Parse(fileName.Replace(".txt", "").Replace("-", ":")));
				alarmClockList.Add(alarmClock!);
			}
			Log.CloseAndFlush();

			return alarmClockList;
		}
	}
}