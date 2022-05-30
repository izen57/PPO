﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;

namespace AlarmClocks {
	internal class AlarmClock {
		public TimeOnly AlarmTime {get; set;}
		public string Name {get; set;}
		public Color AlarmClockColor {get; set;}
		public bool IsWorking {get; set;}

		public AlarmClock(TimeOnly alarmTime, string name, Color alarmClockColor, bool isWorking) {
			AlarmTime = alarmTime;
			Name = name;
			AlarmClockColor = alarmClockColor;
			IsWorking = isWorking;
		}
	}

	internal class AlarmClockService {
		IAlarmClockRepo _repository;

		public AlarmClockService(IAlarmClockRepo repo) {
			_repository = repo;
		}

		public AlarmClock Create(TimeOnly alarmTime, string name, Color alarmClockColor, bool isWorking) {
			AlarmClock alarmClock = new(alarmTime, name, alarmClockColor, isWorking);
			_repository.Create(alarmClock);

			return alarmClock;
		}

		public void Edit(TimeOnly alarmTime, string name, Color alarmClockColor, bool isWorking) {
			_repository.Edit(new AlarmClock(alarmTime, name, alarmClockColor, isWorking));
		}

		public void Delete(AlarmClock alarmClock) {
			_repository.Delete(alarmClock);
		}

		public /*SortedSet<Note> */void List() {
			_repository.GetAllFiles("*");
		}

		public void InvertWork(AlarmClock alarmClock) {
			alarmClock.IsWorking = !alarmClock.IsWorking;
		}

	}

	internal interface IAlarmClockRepo {
		void Create(AlarmClock alarmClock);
		void Edit(AlarmClock alarmClock);
		void Delete(AlarmClock alarmClock);
		List<string> GetAllFiles(string pattern, IsolatedStorageFile storeFile);
	}

	internal class AlarmClockRepo: IAlarmClockRepo {
		IsolatedStorageFile _isoStore;

		public AlarmClockRepo() {
			using (_isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null)) {
				_isoStore.CreateDirectory("/alarmclocks");
				Console.WriteLine("Папка создана.");
			}
		}

		public void Create(AlarmClock alarmClock) {
			if (_isoStore.AvailableFreeSpace <= 0)
				throw new IsolatedStorageException();
			using (StreamWriter TextNote = new("/alarmclocks")) {
				_isoStore.CreateFile("my alarmclock.txt");
				TextNote.WriteLine(alarmClock.AlarmTime);
				TextNote.WriteLine(alarmClock.Name);
				TextNote.WriteLine(alarmClock.AlarmClockColor);
				TextNote.WriteLine(alarmClock.IsWorking);
			}
			Console.WriteLine("Будильник создан.");
		}

		public void Edit(AlarmClock alarmClock) {
			if (_isoStore.FileExists("my alarmclock.txt"))
				using (IsolatedStorageFileStream isoStream = new("TestStore.txt", FileMode.CreateNew, _isoStore)) {
					using (StreamWriter writer = new(isoStream)) {
						writer.WriteLine(alarmClock.AlarmTime);
						writer.WriteLine(alarmClock.Name);
						writer.WriteLine(alarmClock.AlarmClockColor);
						writer.WriteLine(alarmClock.IsWorking);
					}
				}
			Console.WriteLine("Заметка изменена.");
		}

		public void Delete(AlarmClock alarmClock) {
			if (_isoStore.FileExists("my alarmclock.txt"))
				_isoStore.DeleteFile("?"); // как
			Console.WriteLine("Заметка удалена.");
		}

		public List<string> GetAllDirectories(string pattern, IsolatedStorageFile storeFile) {
			// Get the root of the search string.
			string root = Path.GetDirectoryName(pattern);

			if (root != "")
				root += "/";

			// Retrieve directories.
			List<string> directoryList = new(storeFile.GetDirectoryNames(pattern));

			// Retrieve subdirectories of matches.
			for (int i = 0, max = directoryList.Count; i < max; i++) {
				string directory = directoryList[i] + "/";
				List<string> more = GetAllDirectories(root + directory + "*", storeFile);

				// For each subdirectory found, add in the base path.
				for (int j = 0; j < more.Count; j++)
					more[j] += directory;

				// Insert the subdirectories into the list and
				// update the counter and upper bound.
				directoryList.InsertRange(i + 1, more);
				i += more.Count;
				max += more.Count;
			}

			return directoryList;
		}

		public List<string> GetAllFiles(string pattern, IsolatedStorageFile storeFile) {
			// Get the root and file portions of the search string.
			string fileString = Path.GetFileName(pattern);

			List<string> fileList = new(storeFile.GetFileNames(pattern));

			// Loop through the subdirectories, collect matches,
			// and make separators consistent.
			foreach (string directory in GetAllDirectories("*", storeFile))
				foreach (string file in storeFile.GetFileNames(directory + "/" + fileString))
					fileList.Add(directory + "/" + file);

			return fileList;
		} // End of GetFiles.
	}
}