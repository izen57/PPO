using PPO.Database;
using PPO.Logic;
using PPO.Model;

using System.Drawing;
using System.Windows;

namespace TestPPO
{
	[TestClass]
	public class IntegrationTests
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

		[TestMethod]
		public void AlarmClockTest()
		{
			IAlarmClockRepo alarmClockRepo = new AlarmClockFileRepo();
			IAlarmClockService alarmClockService = new AlarmClockService(alarmClockRepo);
			DateTime dateTime = new(2022, 9, 14, 19, 15, 0);

			AlarmClock? check1 = new(dateTime, "check1", Color.FromName("black"), true);
			alarmClockService.Create(check1);
			
			//Assert.AreEqual(check1, alarmClockService.GetAlarmClock(dateTime), "AlarmClockCreate");
			Assert.IsNotNull(alarmClockService.GetAlarmClock(dateTime), "AlarmClockCreate");

			AlarmClock check2 = new(dateTime, "check2", Color.FromName("yellow"), false);
			alarmClockService.Edit(check2);
			Assert.IsNotNull(alarmClockService.GetAlarmClock(dateTime), "AlarmClockCreate");

			alarmClockService.Delete(dateTime);
			Assert.AreEqual(0, alarmClockService.GetAlarmClocks("*").Count, "AlarmClockDelete");
		}

		//[TestMethod]
		//public void StopwatchTest()
		//{
		//	Stopwatch stopwatch = new(
		//		"check1",
		//		Color.FromName("black"),
		//		System.Diagnostics.Stopwatch.StartNew(),
		//		new List<DateTime> { new(2022, 9, 14, 19, 30, 0) },
		//		false
		//	);
		//	IStopwatchService stopwatchService = new StopwatchService(stopwatch);

		//	stopwatchService.Edit()
		//}

	}
}