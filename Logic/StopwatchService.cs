﻿using Model;

using Serilog;
using System.Drawing;

namespace Logic {
	public class StopwatchService: IStopwatchService {
		static Stopwatch _stopwatch;

		public StopwatchService(Stopwatch stopwatch)
		{
			_stopwatch = stopwatch;
		}

		public void Set() {
			_stopwatch.Timing.Start();
			_stopwatch.IsWorking = true;

			Log.Logger.Information($"{DateTime.Now}: Секундомер запущен.");
		}

		public long Stop()
		{
			_stopwatch.Timing.Stop();
			_stopwatch.IsWorking = false;

			Log.Logger.Information($"{DateTime.Now}: Секундомер остановлен.");

			return _stopwatch.Timing.ElapsedMilliseconds;
		}

		public void Reset() {
			_stopwatch.Timing.Reset();

			Log.Logger.Information($"{DateTime.Now}: Секундомер сброшен.");

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

			Log.Logger.Information($"{DateTime.Now}: Цвет секундомера изменён.");
		}
	}
}