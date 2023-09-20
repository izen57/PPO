namespace Exceptions.NotificationExceptions
{
	public class NotificationEditException: Exception
	{
		public NotificationEditException(): base() { }
		public NotificationEditException(string message): base(message) { }
		public NotificationEditException(string message, Exception inner): base(message, inner) { }
	}
}
