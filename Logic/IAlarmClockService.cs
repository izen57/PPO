using PPO.Model;

using System;
using System.Collections.Generic;

namespace PPO.Logic
{
	public interface IAlarmClockService
	{
		public void Create(AlarmClock alarmCloc);
		public void Edit(AlarmClock alarmCloc);
		public void Delete(DateTime alarmTime);
		public List<AlarmClock> GetAlarmClocks(string pattern);
		public void InvertWork(AlarmClock alarmClock);
	}
}
