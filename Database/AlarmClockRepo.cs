using PPO.Model;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;

namespace PPO.Database
{
	public class AlarmClockFileRepo: IAlarmClockRepo
	{
		IsolatedStorageFile _isoStore;

		public AlarmClockFileRepo()
		{
			_isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
			_isoStore.CreateDirectory("alarmclocks");
		}

		public void Create(AlarmClock alarmClock)
		{
			if (_isoStore.AvailableFreeSpace <= 0)
				throw new IsolatedStorageException();
			string filepath = $"alarmclocks/{alarmClock.AlarmTime:dd/MM/yyyy HH-mm-ss}.txt";

			using StreamWriter writer = new(_isoStore.CreateFile(filepath));
			writer.WriteLine(alarmClock.Name);
			writer.WriteLine(alarmClock.AlarmClockColor.Name);
			writer.WriteLine(alarmClock.IsWorking);
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
				throw new FileNotFoundException();
			}

			using (StreamWriter writer = new(isoStream))
			{
				writer.WriteLine(alarmClock.Name);
				writer.WriteLine(alarmClock.AlarmClockColor.Name);
				writer.WriteLine(alarmClock.IsWorking);
			}
			_isoStore.MoveFile($"alarmclocks/{oldTime:dd/MM/yyyy HH-mm-ss}.txt", $"alarmclocks/{alarmClock.AlarmTime:dd/MM/yyyy HH-mm-ss}.txt");
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
				throw new FileNotFoundException();
			}

			isoStream.Close();
			_isoStore.DeleteFile(filepath);
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
				throw new DirectoryNotFoundException();
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
						throw new ArgumentNullException();

					return new AlarmClock(
						alarmTime,
						alarmClockName,
						Color.FromName(alarmClockColor),
						bool.Parse(alarmClockWork)
					);
				}

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
				throw new DirectoryNotFoundException();
			}

			List<AlarmClock> alarmClockList = new();
			foreach (string fileName in filelist)
			{
				try
				{
					var alarmClock = GetAlarmClock(DateTime.Parse(fileName.Replace(".txt", "").Replace("-", ":")));
					alarmClockList.Add(alarmClock!);
				}
				catch (Exception e)
				{
					throw e;
				}
			}

			return alarmClockList;
		}
	}
}