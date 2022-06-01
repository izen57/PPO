﻿using PPO.Database;
using PPO.Model;
using System;
using System.Drawing;

namespace PPO.Logic {
	public class AlarmClockService {
		IAlarmClockRepo _repository;

		public AlarmClockService(IAlarmClockRepo repo) {
			_repository = repo;
		}

		public AlarmClock Create(AlarmClock alarmClock) {
			_repository.Create(alarmClock);

			return alarmClock;
		}

		public void Edit(AlarmClock alarmClock) {
			_repository.Edit(alarmClock);
		}

		public void Delete(TimeOnly alarmTime) {
			_repository.Delete(alarmTime);
		}

		public void List() {
			_repository.GetAllFiles("*");
		}

		public void InvertWork(AlarmClock alarmClock) {
			alarmClock.IsWorking = !alarmClock.IsWorking;
		}
	}
}