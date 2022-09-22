using Data;
using Model;
using Serilog.Core;
using Serilog;

using System;
using System.Collections.Generic;

namespace Logic {
	public class AlarmClockService: IAlarmClockService {
		IAlarmClockRepo _repository;
		Logger _logger = new LoggerConfiguration()
			.WriteTo.File("LogAlarmClock.txt")
			.CreateLogger();

		public AlarmClockService(IAlarmClockRepo repo) {
			_repository = repo ?? throw new ArgumentNullException(nameof(repo));
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

			_logger.Information($"{DateTime.Now}: Будильник остановлен. Время будильника: {alarmClock.AlarmTime}");
		}
	}
}