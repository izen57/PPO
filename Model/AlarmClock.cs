using System;
using System.Drawing;

namespace PPO.Model {
	public class AlarmClock {
		public TimeOnly AlarmTime { get; set; }
		public string Name { get; set; }
		public Color AlarmClockColor { get; set; }
		public bool IsWorking { get; set; }

		public AlarmClock(TimeOnly alarmTime, string name, Color alarmClockColor, bool isWorking) {
			AlarmTime = alarmTime;
			Name = name;
			AlarmClockColor = alarmClockColor;
			IsWorking = isWorking;
		}
	}
}