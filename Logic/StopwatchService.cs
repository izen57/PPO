using PPO.Model;

using System;
using System.Drawing;
using System.Collections.Generic;

namespace PPO.Logic {
	public class StopwatchService: IStopwatchService {
		static Stopwatch _stopwatch;

		public StopwatchService() { }
		public void Set() {
			_stopwatch.Timing.Start();
			_stopwatch.IsWorking = true;
		}

		public void Reset() {
			_stopwatch.Timing.Stop();
			_stopwatch.IsWorking = false;
		}

		public Stopwatch Get() {
			return _stopwatch;
		}

		public void Edit(string name, Color stopwatchColor, System.Diagnostics.Stopwatch timing, List<DateTime> timeFlags) {
			_stopwatch.Name = name;
			_stopwatch.StopwatchColor = stopwatchColor;
			_stopwatch.Timing = timing;
			_stopwatch.TimeFlags = timeFlags;
		}
	}
}