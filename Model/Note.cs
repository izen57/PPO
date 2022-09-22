using System;

namespace PPO.Model {
	public class Note {
		public Guid Id { get; private set; }
		public DateTime CreationTime { get; } = DateTime.Now;
		public string Body { get; set; }
		public bool IsTemporal { get; set; }

		public Note() { }

		public Note(Guid id, string body, bool isTemporal) {
			Id = id;
			Body = body;
			IsTemporal = isTemporal;
		}

		public TimeSpan RemovalTime() {
			return DateTime.Now - CreationTime;
		}
	}
}
