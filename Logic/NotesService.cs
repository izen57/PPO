using PPO.Database;
using PPO.Model;
using Serilog.Core;
using Serilog;

using System;
using System.Collections.Generic;
using System.Timers;

namespace PPO.Logic {
	public class NotesService: INotesService {
		INotesRepo _repository;
		Timer _checkForTime;
		Logger _logger = new LoggerConfiguration()
			.WriteTo.File("LogNote.txt")
			.CreateLogger();

		public NotesService(INotesRepo repo) {
			_repository = repo ?? throw new ArgumentNullException(nameof(repo));

			_checkForTime = new(60 * 1000);
			_checkForTime.Elapsed += new ElapsedEventHandler(AutoDelete);
			_checkForTime.Enabled = true;
		}

		public Note Create(Note note) {
			_repository.Create(note);

			return note;
		}

		public void Edit(Note note) {
			_repository.Edit(note);
		}

		public void Delete(Guid id) {
			_repository.Delete(id);
		}

		public List<Note> GetNotesList(string pattern) {
			return _repository.GetNotesList(pattern);
		}

		public Note? GetNote(Guid guid)
		{
			return _repository.GetNote(guid);
		}

		private void AutoDelete(object sender, ElapsedEventArgs e)
		{
			foreach (Note note in GetNotesList("*"))
				if (note.IsTemporal == true && DateTime.Now - note.CreationTime > TimeSpan.FromDays(1))
				{
					_logger.Information($"{DateTime.Now}: Заметка удалена автоматически по истечении срока. Идентификатор заметки: {note.Id}.");
					_repository.Delete(note.Id);
				}
		}
	}
}
