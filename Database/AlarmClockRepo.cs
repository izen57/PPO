using PPO.Model;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;

namespace PPO.Database
{
	public class AlarmClockFileRepo: IAlarmClockRepo
	{
		IsolatedStorageFile _isoStore;

		public AlarmClockFileRepo()
		{
			_isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
			_isoStore.CreateDirectory("/alarmclocks");
		}

		public void Create(AlarmClock alarmClock)
		{
			if (_isoStore.AvailableFreeSpace <= 0)
				throw new IsolatedStorageException();
			_isoStore.CreateFile($"/alarmclocks/{alarmClock.AlarmTime}.txt");

			StreamWriter TextNote = new($"/alarmclocks/{alarmClock.AlarmTime}.txt");
			TextNote.WriteLine(alarmClock.Name);
			TextNote.WriteLine(alarmClock.AlarmClockColor);
			TextNote.WriteLine(alarmClock.IsWorking);
		}

		public void Edit(AlarmClock alarmClock)
		{
			IsolatedStorageFileStream isoStream;
			try
			{
				isoStream = new(
					$"/alarmclocks/{alarmClock.AlarmTime}.txt",
					FileMode.Open,
					_isoStore
				);
			}
			catch
			{
				throw new FileNotFoundException();
			}

			StreamWriter writer = new(isoStream);

			writer.WriteLine(alarmClock.Name);
			writer.WriteLine(alarmClock.AlarmClockColor);
			writer.WriteLine(alarmClock.IsWorking);
		}

		public void Delete(DateTime alarmTime)
		{
			IsolatedStorageFileStream isoStream;
			try
			{
				isoStream = new(
					$"/alarmclocks/{alarmTime}.txt",
					FileMode.Open,
					_isoStore
				);
			}
			catch
			{
				throw new FileNotFoundException();
			}

			_isoStore.DeleteFile($"/alarmclocks/{alarmTime}.txt");
		}

		public AlarmClock? GetAlarmClock(DateTime alarmTime)
		{
			string[] filelist;
			try
			{
				filelist = _isoStore.GetFileNames($"/alarmclocks/{alarmTime}.txt");
			}
			catch
			{
				throw new DirectoryNotFoundException();
			}

			foreach (string fileName in filelist)
				if (fileName.Equals(alarmTime))
				{
					var readerStream = new StreamReader(fileName);
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
					var alarmClock = GetAlarmClock(DateTime.Parse(fileName));
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