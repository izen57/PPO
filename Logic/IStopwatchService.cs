using System.Drawing;
using Model;

namespace Logic
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
