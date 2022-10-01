﻿using PPO.Database;
using PPO.Model;
using System;
using System.Collections.Generic;

namespace PPO.Logic {
	public class NotesService: INotesService {
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

		public List<Note> GetNotesByPattern(string pattern) {
			return _repository.GetNotesList(pattern);
		}

		public Note? GetNote(Guid guid)
		{
			return _repository.GetNote(guid);
		}
	}
}