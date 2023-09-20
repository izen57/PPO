using Model;

namespace Repositories
{
	public interface INotificationRepo
	{
		void Create(Notification notification);
		void Edit(Notification notification);
		void Delete(Guid id);
		Notification? GetNotification(Guid id);
		List<Notification> GetAllNotificationsList();
	}
}
