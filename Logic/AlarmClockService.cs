using PPO.Database;
using PPO.Model;
using System;
using System.Collections.Generic;
using System.Timers;

namespace PPO.Logic {
	public class AlarmClockService: IAlarmClockService {
		IAlarmClockRepo _repository;
		public Timer _checkForTime;

		public AlarmClockService(IAlarmClockRepo repo) {
			_repository = repo ?? throw new ArgumentNullException(nameof(repo));

			_checkForTime = new(60 * 1000);
			_checkForTime.Enabled = true;
		}

		public void Create(AlarmClock alarmClock) {
			_repository.Create(alarmClock);
		}

		public void Edit(AlarmClock alarmClock, DateTime oldTime) {
			_repository.Edit(alarmClock, oldTime);
		}

		public void Delete(DateTime alarmTime) {
			_repository.Delete(alarmTime);
		}

		public AlarmClock? GetAlarmClock(DateTime dateTime)
		{
			return _repository.GetAlarmClock(dateTime);
		}

		public List<AlarmClock> GetAlarmClocks(string pattern) {
			return _repository.GetAlarmClocksList(pattern);
		}

		public void InvertWork(AlarmClock alarmClock) {
			alarmClock.IsWorking = !alarmClock.IsWorking;
			Edit(alarmClock, alarmClock.AlarmTime);
		}
	}
}