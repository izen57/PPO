namespace Exceptions.NotificationExceptions
{
	public class NotificationGetException: Exception
	{
		public NotificationGetException(): base() { }
		public NotificationGetException(string message): base(message) { }
		public NotificationGetException(string message, Exception inner): base(message, inner) { }
	}
}
