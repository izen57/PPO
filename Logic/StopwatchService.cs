using PPO.Database;
using PPO.Model;

using System;
using System.Drawing;
using System.Collections.Generic;

namespace PPO.Logic {
	internal class StopwatchService {
		Stopwatch stopwatch;

		public StopwatchService() { }
		public void Set() {
			// start watch
		}

		public void Reset() {
			// stop watch
		}

		public void ResetWork() {
			stopwatch.IsWorking = !stopwatch.IsWorking;
		}

		public Stopwatch Get() {
			return stopwatch;
		}

		public void Edit(string name, Color stopwatchColor, TimeOnly timing, List<TimeOnly> timeFlags, bool isWorking) {
			stopwatch.Name = name;
			stopwatch.StopwatchColor = stopwatchColor;
			stopwatch.Timing = timing;
			stopwatch.TimeFlags = timeFlags;
			stopwatch.IsWorking = isWorking;
		}
	}
}