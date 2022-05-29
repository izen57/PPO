using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

namespace Notes {
	internal class Note {
		//private protected int _number;
		public Guid Id {get; private set;}
		//	//	private set {
		//	//		if (value >= 0 && !_checklist.Contains(value)) {
		//	//			_number = value;
		//	//			_checklist.Add(value);
		//	//		} else {
		//	//			Console.WriteLine("Такой идентификатор уже существует.");
		//	//		}
		//	//}
		//}

		public DateTime CreationTime {get;} = DateTime.Now;
		public string Body {get; private set;}
		public bool IsTemporal {get; private set;}

		public Note() {}
		public Note(Guid id, string body, bool isTemporal) {
			Id = id;
			Body = body;
			IsTemporal = isTemporal;
		}

		TimeSpan RemovalTime() {
			return DateTime.Now - CreationTime;
		}
	}

	internal class NotesService {
		INotesRepo _repository;

		public NotesService(INotesRepo repo) {
			_repository = repo;
		}
		public Note Create(string body, bool isTemporal) {
			Note note = new(Guid.NewGuid(), body, isTemporal);
			_repository.Create(note);

			return note;
		}
		// number -> id
		public void Edit(Guid id, string body, bool isTemporal) {
			_repository.Edit(id, body, isTemporal);
		}

		public void Delete(Guid id) {
			_repository.Delete(id);
		}

		public /*SortedSet<Note> */void List() {
			_repository.List();
		}
	}

	internal class NoteInRepo {
		public Guid Id { get; private set; }
		public DateTime CreationTime { get; } = DateTime.Now;
		public string Body { get; set; }
		public bool IsTemporal { get; set; }

		public NoteInRepo() { }
		public NoteInRepo(Guid id, string body, bool isTemporal) {
			Id = id;
			Body = body;
			IsTemporal = isTemporal;
		}
	}

	internal interface INotesRepo {
		void Create(Note note);
		void Edit(Note note);
		void Delete(Note note);
		List<string> GetAllFiles(string pattern, IsolatedStorageFile storeFile);
	}

	internal class NotesRepo: INotesRepo {
		IsolatedStorageFile _isoStore;

		public NotesRepo() {
			using (_isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null)) {
				_isoStore.CreateDirectory("/notes");
				Console.WriteLine("Папка создана");
			}
		}

		public void Create(Note note) {
			// сделать создание файла
			if (_isoStore.AvailableFreeSpace <= 0)
				throw new IsolatedStorageException();
			using (StreamWriter TextNote = new("/notes")) {
				_isoStore.CreateFile("my note.txt");
			}
			Console.WriteLine("Заметка создана.");
		}

		public void Edit(Note note) {
			if (_isoStore.FileExists("my note.txt"))
				using (IsolatedStorageFileStream isoStream = new("TestStore.txt", FileMode.CreateNew, _isoStore)) {
					using (StreamWriter writer = new(isoStream)) {
						writer.WriteLine("Hello Isolated Storage");
						Console.WriteLine("You have written to the file.");
					}
				}
			Console.WriteLine("Заметка изменена.");
		}

		public void Delete(Note note) {
			if (_isoStore.FileExists("my note.txt"))
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
