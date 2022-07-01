using PPO.Database;
using PPO.Model;
using System;

namespace PPO.Logic {
	public class NotesService {
		INotesRepo _repository;

		public NotesService(INotesRepo repo) {
			_repository = repo ?? throw new ArgumentNullException(nameof(repo));
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

		public void List() {
			_repository.GetAllNotes("*");
		}
	}
}
