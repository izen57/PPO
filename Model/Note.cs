using System;

namespace PPO.Model {
	public class Note {
		public Guid Id { get; private set; }
		public DateTime CreationTime { get; } = DateTime.Now;
		public string Body { get; private set; }
		public bool IsTemporal { get; private set; }

		public Note() { }

		public Note(Guid id, string body, bool isTemporal) {
			Id = id;
			Body = body;
			IsTemporal = isTemporal;
		}

		TimeSpan RemovalTime() {
			return DateTime.Now - CreationTime;
		}
	}
}
