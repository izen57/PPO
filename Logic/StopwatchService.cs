using PPO.Database;
using PPO.Model;

using System;
using System.Drawing;
using System.Collections.Generic;

namespace PPO.Logic {
	public class StopwatchService {
		static Stopwatch _stopwatch;

		public StopwatchService() { }
		public void Set() {
			_stopwatch.Timing.Start();
		}

		public void Reset() {
			_stopwatch.Timing.Stop();
		}

		public void ResetWork() {
			_stopwatch.IsWorking = !_stopwatch.IsWorking;
		}

		public Stopwatch Get() {
			return _stopwatch;
		}

		public void Edit(string name, Color stopwatchColor, System.Diagnostics.Stopwatch timing, List<TimeOnly> timeFlags, bool isWorking) {
			_stopwatch.Name = name;
			_stopwatch.StopwatchColor = stopwatchColor;
			_stopwatch.Timing = timing;
			_stopwatch.TimeFlags = timeFlags;
			_stopwatch.IsWorking = isWorking;
		}
	}
}