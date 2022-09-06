using PPO.Model;
using System.Collections.Generic;
using System;

namespace PPO.Logic
{
	public interface INotesService
	{
		public Note Create(Note note);
		public void Edit(Note note);
		public void Delete(Guid id);
		public List<Note> GetNotes(string pattern);
	}
}
