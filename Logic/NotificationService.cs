using Model;

using Repositories;

using Serilog;

namespace Logic
{
	public class NotificationService: INotificationService
	{
		INotificationRepo _repository;

		public NotificationService(INotificationRepo repo)
		{
			Log.Logger = new LoggerConfiguration()
				.WriteTo.File("LogNote.txt")
				.CreateLogger();

			_repository = repo ?? throw new ArgumentNullException(nameof(repo));
		}

		public void Create(Notification notification)
		{
			_repository.Create(notification);
		}

		public void Edit(Notification notification)
		{
			_repository.Edit(notification);
		}

		public void Delete(Guid id)
		{
			_repository.Delete(id);
		}

		public List<Notification> GetAllNotificationsList()
		{
			return _repository.GetAllNotificationsList();
		}

		public Notification? GetNotification(Guid guid)
		{
			return _repository.GetNotification(guid);
		}
	}
}
