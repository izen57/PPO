using PPO.Model;

using System;
using System.Collections.Generic;

namespace PPO.Logic
{
	public interface IAlarmClockService
	{
		void Create(AlarmClock alarmCloc);
		void Edit(AlarmClock alarmCloc);
		void Delete(DateTime alarmTime);
		AlarmClock? GetAlarmClock(DateTime dateTime);
		List<AlarmClock> GetAlarmClocks(string pattern);
		void InvertWork(AlarmClock alarmClock);
	}
}
