using PPO.Model;
using System.Collections.Generic;
using System;

namespace PPO.Logic
{
	public interface INotesService
	{
		Note Create(Note note);
		void Edit(Note note);
		void Delete(Guid id);
		List<Note> GetNotesByPattern(string pattern);
		Note? GetNote(Guid guid);
	}
}
