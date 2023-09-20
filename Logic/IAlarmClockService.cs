using Model;

namespace Logic
{
	public interface IAlarmClockService
	{
		void Create(AlarmClock alarmClock);
		void Edit(AlarmClock alarmClock, DateTime oldTime);
		void Delete(DateTime alarmTime);
		AlarmClock? GetAlarmClock(DateTime dateTime);
		List<AlarmClock> GetAllAlarmClocks();
		void InvertWork(AlarmClock alarmClock);
	}
}
