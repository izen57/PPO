using Model;
using System.IO.IsolatedStorage;
using Serilog;
using Exceptions.NoteExceptions;
using Repositories;

namespace RepositoriesImplementations
{
	public class NoteFileRepo: INoteRepo
	{
		IsolatedStorageFile _isoStore;

		public NoteFileRepo()
		{
			_isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
			_isoStore.CreateDirectory("notes");
			Log.Logger.Information($"{DateTime.Now}: Создана папка для заметок.");
		}

		public void Create(Note note)
		{
			if (_isoStore.AvailableFreeSpace <= 0)
			{
				Log.Logger.Error($"{DateTime.Now}: Место в папке заметок закончилось.");
				throw new NoteCreateException(
					"NoteCreate: Место в папке заметок закончилось.",
					new IsolatedStorageException()
				);
			}

			using StreamWriter TextNote = new(_isoStore.CreateFile($"notes/{note.Id}.txt"));
			TextNote.WriteLine(note.CreationTime);
			TextNote.WriteLine(note.Body);
			TextNote.WriteLine(note.IsTemporal);
			Log.Logger.Error($"{DateTime.Now}: Создан файл заметки со следующей информацией:" +
				$"{note.Id}," +
				$"{note.CreationTime}," +
				$"{note.Body}," +
				$"{note.IsTemporal}."
			);
		}

		public void Edit(Note note)
		{
			IsolatedStorageFileStream isoStream;
			try
			{
				isoStream = new(
					$"notes/{note.Id}.txt",
					FileMode.Create,
					FileAccess.Write,
					_isoStore
				);
			}
			catch (Exception ex)
			{
				Log.Logger.Error($"{DateTime.Now}: Файл с названием \"notes/{note.Id}.txt\" не найден.");
				throw new NoteEditException(
					$"NoteEdit: Файл с названием \"notes/{note.Id}.txt\" не найден.",
					ex
				);
			}

			using StreamWriter writer = new(isoStream);

			writer.WriteLine(note.CreationTime);
			writer.WriteLine(note.Body);
			writer.WriteLine(note.IsTemporal);

			Log.Logger.Information($"{DateTime.Now}: Изменён файл заметки со следующей информацией:" +
				$"{note.Id}," +
				$"{note.CreationTime}," +
				$"{note.Body}," +
				$"{note.IsTemporal}."
			);
		}

		public void Delete(Guid Id)
		{
			IsolatedStorageFileStream isoStream;
			try
			{
				isoStream = new(
					$"notes/{Id}.txt",
					FileMode.Open,
					FileAccess.Write,
					_isoStore
				);
			}
			catch (Exception ex)
			{
				Log.Logger.Error($"{DateTime.Now}: Файл с названием \"notes/{Id}.txt\" не найден.");
				throw new NoteDeleteException(
					$"NoteDelete: Файл с названием \"notes/{Id}.txt\" не найден.",
					ex
				);
			}

			isoStream.Close();
			_isoStore.DeleteFile($"notes/{Id}.txt");

			Log.Logger.Information($"{DateTime.Now}: Удалён файл заметки. Идентификатор заметки: {Id}.");
		}

		public Note? GetNote(Guid Id)
		{
			string[] filelist;
			try
			{
				filelist = _isoStore.GetFileNames($"notes/{Id}.txt");
			}
			catch (Exception ex)
			{
				Log.Logger.Error($"{DateTime.Now}: Папка для заметок в защищённом хранилище не найдена.");
				throw new NoteGetException(
					"NoteGet: Папка для заметок в защищённом хранилище не найдена.",
					ex
				);
			}

			foreach (string fileName in filelist)
				if (fileName.Replace(".txt", "") == Id.ToString())
				{
					using var readerStream = new StreamReader(new IsolatedStorageFileStream(
						$"notes/{Id}.txt",
						FileMode.Open,
						FileAccess.Read,
						_isoStore
					));
					string? noteCreationTime = readerStream.ReadLine();
					string? noteBody = readerStream.ReadLine();
					string? noteIsTemporal = readerStream.ReadLine();
					if (noteCreationTime == null || noteBody == null || noteIsTemporal == null)
					{
						Log.Logger.Error($"{DateTime.Now}: Ошибка разметки файла заметки. Идентификатор заметки: {Id}.");
						throw new ArgumentNullException();
					}

					return new Note(
						Id,
						noteBody,
						bool.Parse(noteIsTemporal)
					);
				}

			return null;
		}

		public List<Note> GetAllNotesList()
		{
			string[] filelist;
			try
			{
				filelist = _isoStore.GetFileNames("notes/");
			}
			catch (Exception ex)
			{
				Log.Logger.Error($"{DateTime.Now}: Папка для заметок в защищённом хранилище не найдена.");
				throw new NoteGetException(
					"NoteGet: Папка для заметок в защищённом хранилище не найдена.",
					ex
				);
			}

			List<Note> noteList = new();
			foreach (string fileName in filelist)
			{
				var note = GetNote(Guid.Parse(fileName.Replace(".txt", "")));
				noteList.Add(note!);
			}

			return noteList;
		}
	}
}
