﻿using PPO.Database;
using PPO.Logic;
using PPO.Model;

namespace TestPPO
{
	[TestClass]
	public class IntegrationNoteTests
	{
		[TestMethod]
		public void NoteTest()
		{
			INotesRepo notesRepo = new NotesFileRepo();
			INotesService notesService = new NotesService(notesRepo);
			var id = Guid.NewGuid();
			
			Note check1 = new(id, "test1", false);
			notesService.Create(check1);
			Assert.IsNotNull(check1, "NoteCreate");

			Note check2 = new(id, "changed body", false);
			notesService.Edit(check2);
			Assert.IsNotNull(check2, "NoteChange");

			notesService.Delete(id);
			Assert.AreEqual(0, notesService.GetNotesByPattern("*").Count, "AlarmClockDelete");
		}
	}
}