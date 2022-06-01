using PPO.Database;
using PPO.Model;

using System;

namespace PPO.Logic {
	public class NotesService {
		INotesRepo _repository;

		public NotesService(INotesRepo repo) {
			_repository = repo ?? throw new ArgumentNullException(nameof(repo));
		}

		public Note Create(/*string body, bool isTemporal*/Note note) {
			//Note note = new(Guid.NewGuid(), body, isTemporal);
			_repository.Create(note);

			return note;
		}

		public void Edit(/*Guid id, string body, bool isTemporal*/Note note) {
			_repository.Edit(/*new Note(id, body, isTemporal)*/note);
		}

		public void Delete(Guid id) {
			_repository.Delete(id);
		}

		public /*SortedSet<Note> */void List() {
			_repository.GetAllFiles("*");
		}
	}
}
