using System;
using System.Drawing;
using System.Collections.Generic;

namespace PPO.Model {
	public class Stopwatch {
		public string Name { get; set; }
		public Color StopwatchColor { get; set; }
		public System.Diagnostics.Stopwatch Timing { get; set; }
		public List<DateTime> TimeFlags { get; set; }
		public bool IsWorking { get; set; }

		public Stopwatch(string name, Color stopwatchColor, System.Diagnostics.Stopwatch timing, List<DateTime> timeFlags, bool isWorking) {
			Name = name;
			StopwatchColor = stopwatchColor;
			Timing = timing;
			TimeFlags = timeFlags;
			IsWorking = isWorking;
		}
	}
}