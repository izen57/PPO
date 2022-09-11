using PPO.Database;
using PPO.Model;
using System;
using System.Collections.Generic;

namespace PPO.Logic {
	public class AlarmClockService: IAlarmCLockService {
		IAlarmClockRepo _repository;

		public AlarmClockService(IAlarmClockRepo repo) {
			_repository = repo ?? throw new ArgumentNullException(nameof(repo));
		}

		public void Create(AlarmClock alarmClock) {
			_repository.Create(alarmClock);
		}

		public void Edit(AlarmClock alarmClock) {
			_repository.Edit(alarmClock);
		}

		public void Delete(DateTime alarmTime) {
			_repository.Delete(alarmTime);
		}

		public List<AlarmClock> GetAlarmClocks(string pattern) {
			return _repository.GetAlarmClocks(pattern);
		}

		public void InvertWork(AlarmClock alarmClock) {
			alarmClock.IsWorking = !alarmClock.IsWorking;
			Edit(alarmClock);
		}
	}
}