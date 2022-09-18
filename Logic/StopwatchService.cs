using PPO.Model;

using System;
using System.Drawing;
using System.Collections.Generic;

namespace PPO.Logic {
	public class StopwatchService: IStopwatchService {
		private static Stopwatch _stopwatch;

		public StopwatchService(Stopwatch stopwatch)
		{
			_stopwatch = stopwatch;
		}

		public void Set() {
			_stopwatch.Timing.Start();
			_stopwatch.IsWorking = true;
		}

		public long Reset() {
			_stopwatch.Timing.Stop();
			_stopwatch.IsWorking = false;

			return _stopwatch.Timing.ElapsedMilliseconds;
		}

		public long SetFlag(DateTime dateTime)
		{
			_stopwatch.TimeFlags.Add(dateTime);
			return _stopwatch.Timing.ElapsedMilliseconds;
		}

		public Stopwatch Get() {
			return _stopwatch;
		}

		public void Edit(string name, Color stopwatchColor, System.Diagnostics.Stopwatch timing, SortedSet<DateTime> timeFlags) {
			_stopwatch.Name = name;
			_stopwatch.StopwatchColor = stopwatchColor;
			_stopwatch.Timing = timing;
			_stopwatch.TimeFlags = timeFlags;
		}
	}
}