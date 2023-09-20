namespace Exceptions.NotificationExceptions
{
	public class NotificationCreateException: Exception
	{
		public NotificationCreateException(): base() { }
		public NotificationCreateException(string message): base(message) { }
		public NotificationCreateException(string message, Exception inner): base(message, inner) { }
	}
}
