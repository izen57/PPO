using PPO.Model;

using System;
using System.Drawing;

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

		public long Stop()
		{
			_stopwatch.Timing.Stop();
			_stopwatch.IsWorking = false;
			return _stopwatch.Timing.ElapsedMilliseconds;
		}

		public void Reset() {
			_stopwatch.Timing.Reset();
			_stopwatch.IsWorking = false;
		}

		public long SetFlag()
		{
			_stopwatch.TimeFlags.Add(DateTime.Now);
			return _stopwatch.Timing.ElapsedMilliseconds;
		}

		public Stopwatch Get() {
			return _stopwatch;
		}

		public void EditColor(Color stopwatchColor) {
			_stopwatch.StopwatchColor = stopwatchColor;
		}
	}
}