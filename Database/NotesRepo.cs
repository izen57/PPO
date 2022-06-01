//using PPO.Model;
//using System;
//using System.IO;
//using System.IO.IsolatedStorage;

//namespace PPO.Database {
//	public class NotesRepo: INotesRepo {
//		IsolatedStorageFile _isoStore;

//		public NotesRepo() {
//			_isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
//			_isoStore.CreateDirectory("/notes");
//			Console.WriteLine("Папка создана");
//		}

//		public void Create(Note note) {
//			// сделать создание файла
//			if (_isoStore.AvailableFreeSpace <= 0)
//				throw new IsolatedStorageException();

//			using StreamWriter TextNote = new("/notes");
//			_isoStore.CreateFile("my note.txt");
//			TextNote.WriteLine(note.Id);
//			TextNote.WriteLine(note.CreationTime);
//			TextNote.WriteLine(note.Body);
//			Console.WriteLine("Заметка создана.");
//		}

//		public void Edit(Note note) {
//			using IsolatedStorageFileStream isoStream = new("/notes/my note.txt", FileMode.Open, _isoStore);
//			using StreamReader reader = new(isoStream);
//			using StreamWriter writer = new(isoStream);

//			if (_isoStore.FileExists("my note.txt"))
//				foreach (string file in GetAllFiles("notes")) {
//					string first_string = reader.ReadLine();
//					if (first_string.Equals(note.Id.ToString())) {
//						writer.WriteLine(note.Body);
//						writer.WriteLine(note.IsTemporal);
//					}
//				}
//			Console.WriteLine("Заметка изменена.");
//		}

//		public void Delete(Guid id) {
//			using IsolatedStorageFileStream isoStream = new("/notes/my note.txt", FileMode.CreateNew, _isoStore);
//			using StreamReader reader = new(isoStream);

//			if (_isoStore.FileExists("my note.txt"))
//				foreach (string file in GetAllFiles("notes")) {
//					string first_string = reader.ReadLine();
//					if (first_string.Equals(id))
//						_isoStore.DeleteFile(file);
//				}
//			Console.WriteLine("Заметка удалена.");
//		}

//		public List<string> GetAllDirectories(string pattern) {
//			// Get the root of the search string.
//			string root = Path.GetDirectoryName(pattern);

//			if (root != "")
//				root += "/";

//			// Retrieve directories.
//			List<string> directoryList = new(_isoStore.GetDirectoryNames(pattern));

//			// Retrieve subdirectories of matches.
//			for (int i = 0, max = directoryList.Count; i < max; i++) {
//				string directory = directoryList[i] + "/";
//				List<string> more = GetAllDirectories(root + directory + "*");

//				// For each subdirectory found, add in the base path.
//				for (int j = 0; j < more.Count; j++)
//					more[j] += directory;

//				// Insert the subdirectories into the list and
//				// update the counter and upper bound.
//				directoryList.InsertRange(i + 1, more);
//				i += more.Count;
//				max += more.Count;
//			}

//			return directoryList;
//		}

//		public List<string> GetAllFiles(string pattern) {
//			// Get the root and file portions of the search string.
//			string fileString = Path.GetFileName(pattern);

//			List<string> fileList = new(_isoStore.GetFileNames(pattern));

//			// Loop through the subdirectories, collect matches,
//			// and make separators consistent.
//			foreach (string directory in GetAllDirectories("*"))
//				foreach (string file in _isoStore.GetFileNames(directory + "/" + fileString))
//					fileList.Add(directory + "/" + file);

//			return fileList;
//		} // End of GetFiles.
//	}
//}
