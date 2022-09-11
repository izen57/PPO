using System.Collections.Generic;
using System;
using System.Drawing;

namespace PPO.Logic
{
	public interface IStopwatchService
	{
		public void Set();
		public void Reset();
		public void Edit(string name, Color stopwatchColor, System.Diagnostics.Stopwatch timing, List<DateTime> timeFlags);
	}
}
