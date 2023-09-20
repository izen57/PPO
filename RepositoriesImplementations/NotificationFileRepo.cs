using Exceptions.AlarmClockExceptions;
using Exceptions.NotificationExceptions;

using Model;

using Repositories;

using Serilog;

using System.Drawing;
using System.IO.IsolatedStorage;

namespace RepositoriesImplementations
{
	public class NotificationFileRepo: INotificationRepo
	{
		IsolatedStorageFile _isoStore;

		public NotificationFileRepo()
		{
			try {
				_isoStore = IsolatedStorageFile.GetUserStoreForAssembly();
				_isoStore.CreateDirectory("notifications");
			} catch (Exception ex) {
				Log.Logger.Error("Папку напоминаний не удалось создать.");
				throw new NotificationCreateException(
					"NotificationFileRepo: Невозможно создать защищённое хранилище напоминаний.",
					ex
				);
			}
			Log.Logger.Information("Создана папка для напоминаний.");
		}

		public void Create(Notification notification)
		{
			if (_isoStore.AvailableFreeSpace <= 0) {
				Log.Logger.Error("Место в папке напоминаний закончилось.");
				throw new NotificationCreateException(
					"NoteCreate: Место в папке напоминаний закончилось.",
					new IsolatedStorageException()
				);
			}

			IsolatedStorageFileStream isoStream;
			try {
				isoStream = _isoStore.CreateFile($"notifications/{notification.Id}.txt");
			} catch (Exception ex) {
				Log.Logger.Error($"Файл с названием \"notifications/{notification.Id}.txt\" нельзя открыть.");
				throw new AlarmClockEditException(
					$"Файл с названием \"notifications/{notification.Id}.txt\" нельзя открыть.",
					ex
				);
			}

			using StreamWriter TextNote = new(isoStream);
			TextNote.WriteLine(notification.NotifyTime);
			TextNote.WriteLine(notification.Body);
			TextNote.WriteLine(notification.NotificationColor.Name);
			TextNote.WriteLine(notification.IsWorking);
			Log.Logger.Error(
				$"Создан файл заметки со следующей информацией:\n" +
				$"{notification.Id}," +
				$"{notification.NotifyTime}," +
				$"{notification.Body}," +
				$"{notification.NotificationColor.Name}," +
				$"{notification.IsWorking}."
			);
		}

		public void Delete(Guid id)
		{
			IsolatedStorageFileStream isoStream;
			try {
				isoStream = new(
					$"notifications/{id}.txt",
					FileMode.Open,
					FileAccess.Write,
					_isoStore
				);
			} catch (Exception ex) {
				Log.Logger.Error($"{DateTime.Now}: Файл с названием \"notifications/{id}.txt\" не найден.");
				throw new NotificationDeleteException(
					$"NoteDelete: Файл с названием \"notifications/{id}.txt\" не найден.",
					ex
				);
			}

			isoStream.Close();
			_isoStore.DeleteFile($"notifications/{id}.txt");

			Log.Logger.Information($"Удалён файл напоминания. Идентификатор напоминания: {id}.");
		}

		public void Edit(Notification notification)
		{
			IsolatedStorageFileStream isoStream;
			try {
				isoStream = new(
					$"notifications/{notification.Id}.txt",
					FileMode.Create,
					FileAccess.Write,
					_isoStore
				);
			} catch (Exception ex) {
				Log.Logger.Error($"Файл с названием \"notifications/{notification.Id}.txt\" не найден.");
				throw new NotificationEditException(
					$"NoteEdit: Файл с названием \"notifications/{notification.Id}.txt\" не найден.",
					ex
				);
			}

			using StreamWriter writer = new(isoStream);

			writer.WriteLine(notification.NotifyTime);
			writer.WriteLine(notification.Body);
			writer.WriteLine(notification.NotificationColor.Name);
			writer.WriteLine(notification.IsWorking);

			Log.Logger.Information("Изменён файл напоминания со следующей информацией:\n" +
				$"{notification.Id}," +
				$"{notification.NotifyTime}," +
				$"{notification.Body}," +
				$"{notification.NotificationColor.Name}," +
				$"{notification.IsWorking}."
			);
		}

		public List<Notification> GetAllNotificationsList()
		{
			string[] filelist;
			try {
				filelist = _isoStore.GetFileNames("notifications/");
			} catch (Exception ex) {
				Log.Logger.Error("Папка для напоминаний в защищённом хранилище не найдена.");
				throw new NotificationGetException(
					"NoteGet: Папка для напоминаний в защищённом хранилище не найдена.",
					ex
				);
			}

			List<Notification> notificationList = new();
			foreach (string fileName in filelist) {
				var notification = GetNotification(Guid.Parse(fileName.Replace(".txt", "")));
				notificationList.Add(notification!);
			}

			return notificationList;
		}

		public Notification? GetNotification(Guid id)
		{
			string[] filelist;
			try {
				filelist = _isoStore.GetFileNames($"notifications/{id}.txt");
			} catch (Exception ex) {
				Log.Logger.Error($"Папка для заметок в защищённом хранилище не найдена.");
				throw new NotificationGetException(
					"GetNote: Папка для заметок в защищённом хранилище не найдена.",
					ex
				);
			}

			foreach (string fileName in filelist)
				if (fileName.Replace(".txt", "") == id.ToString()) {
					using var readerStream = new StreamReader(new IsolatedStorageFileStream(
						$"notifications/{id}.txt",
						FileMode.Open,
						FileAccess.Read,
						_isoStore
					));
					string? notifyTime = readerStream.ReadLine();
					string? body = readerStream.ReadLine();
					string? colorName = readerStream.ReadLine();
					string? isWorking = readerStream.ReadLine();
					if (notifyTime == null || body == null || colorName == null || isWorking == null) {
						Log.Logger.Error($"Ошибка чтения файла напоминания. Идентификатор напоминания: {id}.");
						throw new ArgumentNullException();
					}

					return new Notification(
						id,
						DateTime.Parse(notifyTime),
						body,
						Color.FromName(colorName),
						bool.Parse(isWorking)
					);
				}

			return null;
		}
	}
}
