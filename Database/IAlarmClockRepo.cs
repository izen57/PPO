using PPO.Model;
using System;
using System.Collections.Generic;

namespace PPO.Database {
	public interface IAlarmClockRepo {
		void Create(AlarmClock alarmClock);
		void Edit(AlarmClock alarmClock, DateTime oldTime);
		void Delete(DateTime alarmTime);
		AlarmClock? GetAlarmClock(DateTime alarmTime);
		List<AlarmClock> GetAlarmClocksList(string pattern);
	}
}