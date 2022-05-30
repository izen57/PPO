using System;
using System.Drawing;
using System.Collections.Generic;

namespace Stopwatches {
	internal class Stopwatch {
		public string Name {get; set;}
		public Color StopwatchColor {get; set;}
		public TimeOnly Timing {get; set;}
		public List<TimeOnly> TimeFlags {get; set;}
		public bool IsWorking {get; set;}

		public Stopwatch(string name, Color stopwatchColor, TimeOnly timing, List<TimeOnly> timeFlags, bool isWorking) {
			Name = name;
			StopwatchColor = stopwatchColor;
			Timing = timing;
			TimeFlags = timeFlags;
			IsWorking = isWorking;
		}
	}

	internal class StopwatchService {
		Stopwatch stopwatch;
		
		public StopwatchService() {}
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