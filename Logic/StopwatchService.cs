using Model;

using Serilog.Core;
using Serilog;

using System;
using System.Drawing;

namespace Logic {
	public class StopwatchService: IStopwatchService {
		static Stopwatch _stopwatch;
		Logger _logger = new LoggerConfiguration()
			.WriteTo.File("LogStopwatch.txt")
			.CreateLogger();

		public StopwatchService(Stopwatch stopwatch)
		{
			_stopwatch = stopwatch;
		}

		public void Set() {
			_stopwatch.Timing.Start();
			_stopwatch.IsWorking = true;

			_logger.Information($"{DateTime.Now}: Секундомер запущен.");
		}

		public long Stop()
		{
			_stopwatch.Timing.Stop();
			_stopwatch.IsWorking = false;

			_logger.Information($"{DateTime.Now}: Секундомер остановлен.");

			return _stopwatch.Timing.ElapsedMilliseconds;
		}

		public void Reset() {
			_stopwatch.Timing.Reset();

			_logger.Information($"{DateTime.Now}: Секундомер сброшен.");

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

			_logger.Information($"{DateTime.Now}: Цвет секундомера изменён.");
		}
	}
}