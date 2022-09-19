using System.Drawing;
using PPO.Model;

namespace PPO.Logic
{
	public interface IStopwatchService
	{
		void Set();
		void Reset();
		long Stop();
		long SetFlag();
		Stopwatch Get();
		public void EditColor(Color stopwatchColor);
	}
}
