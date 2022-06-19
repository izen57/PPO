using PPO.Model;
using System;
using System.Collections.Generic;

namespace PPO.Database {
	public interface IAlarmClockRepo {
		void Create(AlarmClock alarmClock);
		void Edit(AlarmClock alarmClock);
		void Delete(DateTime alarmTime);
		List<string> GetAllFiles(string pattern);
	}
}