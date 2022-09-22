using Model;

namespace Logic
{
	public interface INoteService
	{
		void Create(Note note);
		void Edit(Note note);
		void Delete(Guid id);
		List<Note> GetNotesList(string pattern);
		Note? GetNote(Guid guid);
	}
}
