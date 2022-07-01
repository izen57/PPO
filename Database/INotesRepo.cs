using PPO.Model;
using System;
using System.Collections.Generic;

namespace PPO.Database {
	public interface INotesRepo {
		void Create(Note note);
		void Edit(Note note);
		void Delete(Guid id);
		List<string> GetAllNotes(string pattern);
	}
}
