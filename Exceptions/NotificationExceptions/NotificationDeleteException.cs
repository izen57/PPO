namespace Exceptions.NotificationExceptions
{
	public class NotificationDeleteException: Exception
	{
		public NotificationDeleteException(): base() { }
		public NotificationDeleteException(string message): base(message) { }
		public NotificationDeleteException(string message, Exception inner): base(message, inner) { }
	}
}
