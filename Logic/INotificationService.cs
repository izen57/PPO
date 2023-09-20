using Model;

namespace Logic
{
	public interface INotificationService
	{
		void Create(Notification notification);
		void Edit(Notification notification);
		void Delete(Guid id);
		Notification? GetNotification(Guid id);
		List<Notification> GetAllNotificationsList();
	}
}
