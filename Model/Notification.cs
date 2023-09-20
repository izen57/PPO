using System.Drawing;

namespace Model
{
	public class Notification
	{
		public Guid Id { get; private set; }
		public DateTime NotifyTime { get; set; }
		public string Body { get; set; }
		public Color NotificationColor { get; set; }
		public bool IsWorking { get; set; }

		public Notification(Guid id, DateTime notifyTime, string body, Color notificationColor, bool isWorking)
		{
			Id = id;
			NotifyTime = notifyTime;
			Body = body;
			NotificationColor = notificationColor;
			IsWorking = isWorking;
		}
	}
}
