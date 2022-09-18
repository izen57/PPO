using System.Collections.Generic;
using System;
using System.Drawing;

namespace PPO.Logic
{
	public interface IStopwatchService
	{
		void Set();
		long Reset();
		void Edit(string name, Color stopwatchColor, System.Diagnostics.Stopwatch timing, SortedSet<DateTime> timeFlags);
	}
}
