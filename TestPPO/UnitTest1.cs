using Moq;
using PPO.Database;
using PPO.Logic;
using PPO.Model;

using System.Drawing;
using systemStopwatch = System.Diagnostics.Stopwatch;

namespace TestPPO {
	[TestClass]
	public class UnitTestNote {
		[TestMethod]
		public void TestDelete() {
			var id = Guid.NewGuid();

			var repoMock = new Mock<INotesRepo>();
			repoMock.Setup(x => x.Delete(id)).Verifiable();

			var service = new NotesService(repoMock.Object);
			service.Delete(id);

			repoMock.Verify(x => x.Delete(id));
		}

		[TestMethod]
		public void TestCreate() {
			Note note = new(Guid.NewGuid(), "моя первая заметка", false);

			var repoMock = new Mock<INotesRepo>();
			repoMock.Setup(x => x.Create(note)).Verifiable();

			var service = new NotesService(repoMock.Object);
			service.Create(note);

			repoMock.Verify(x => x.Create(note));
		}

		[TestMethod]
		public void TestEdit() {
			Note note = new(Guid.NewGuid(), "моя первая заметка", false);

			var repoMock = new Mock<INotesRepo>();
			repoMock.Setup(x => x.Edit(note)).Verifiable();

			var service = new NotesService(repoMock.Object);
			service.Edit(note);

			repoMock.Verify(x => x.Edit(note));
		}
	}

	[TestClass]
	public class UnitTestAlarmClock {
		[TestMethod]
		public void TestDelete() {
			DateTime time = new(2, 14, 18);

			var repoMock = new Mock<IAlarmClockRepo>();
			repoMock.Setup(x => x.Delete(time)).Verifiable();

			var service = new AlarmClockService(repoMock.Object);
			service.Delete(time);

			repoMock.Verify(x => x.Delete(time));
		}

		[TestMethod]
		public void TestCreate() {
			AlarmClock alarmClock = new(new(2, 14, 18), "first", Color.AliceBlue, true);

			var repoMock = new Mock<IAlarmClockRepo>();
			repoMock.Setup(x => x.Create(alarmClock)).Verifiable();

			var service = new AlarmClockService(repoMock.Object);
			service.Create(alarmClock);

			repoMock.Verify(x => x.Create(alarmClock));
		}

		[TestMethod]
		public void TestEdit() {
			AlarmClock alarmClock = new(new(2, 14, 18), "first", Color.AliceBlue, true);

			var repoMock = new Mock<IAlarmClockRepo>();
			repoMock.Setup(x => x.Edit(alarmClock)).Verifiable();

			var service = new AlarmClockService(repoMock.Object);
			service.Edit(alarmClock);

			repoMock.Verify(x => x.Edit(alarmClock));
		}
	}
}