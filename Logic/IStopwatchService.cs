using System.Collections.Generic;
using System;
using System.Drawing;

namespace PPO.Logic
{
	public interface IStopwatchService
	{
		void Set();
		void Reset();
		void Edit(string name, Color stopwatchColor, System.Diagnostics.Stopwatch timing, List<DateTime> timeFlags);
	}
}
