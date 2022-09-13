using PPO.Model;
using System;
using System.IO;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace PPO.Database
{
	public class NotesFileRepo: INotesRepo
	{
		IsolatedStorageFile _isoStore;

		public NotesFileRepo()
		{
			_isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
			_isoStore.CreateDirectory("notes");
		}

		public void Create(Note note)
		{
			if (_isoStore.AvailableFreeSpace <= 0)
				throw new IsolatedStorageException();

			using StreamWriter TextNote = new(_isoStore.CreateFile($"notes/{note.Id}.txt"));
			TextNote.WriteLine(note.CreationTime);
			TextNote.WriteLine(note.Body);
			TextNote.WriteLine(note.IsTemporal);
		}

		public void Edit(Note note)
		{
			IsolatedStorageFileStream isoStream;
			try
			{
				isoStream = new(
					$"notes/{note.Id}.txt",
					FileMode.Open,
					FileAccess.Write,
					_isoStore
				);
			}
			catch
			{
				throw new FileNotFoundException();
			}

			using StreamWriter writer = new(isoStream);

			writer.WriteLine(note.CreationTime);
			writer.WriteLine(note.Body);
			writer.WriteLine(note.IsTemporal);
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
			catch
			{
				throw new FileNotFoundException();
			}

			isoStream.Close();
			_isoStore.DeleteFile($"notes/{Id}.txt");
		}

		public Note? GetNote(Guid Id)
		{
			string[] filelist;
			try
			{
				filelist = _isoStore.GetFileNames($"notes/{Id}.txt");
			}
			catch
			{
				throw new DirectoryNotFoundException();
			}

			foreach (string fileName in filelist)
				if (fileName.Equals(Id))
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
						throw new ArgumentNullException();

					return new Note(
						Id,
						noteBody,
						bool.Parse(noteIsTemporal)
					);
				}

			return null;
		}

		public List<Note> GetNotesList(string pattern)
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

			List<Note> noteList = new();
			foreach (string fileName in filelist)
			{
				try
				{
					var note = GetNote(Guid.Parse(fileName));
					noteList.Add(note!);
				}
				catch (Exception e)
				{
					throw e;
				}
			}

			return noteList;
		}
	}
}
