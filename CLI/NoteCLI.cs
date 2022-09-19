using PPO.Database;
using PPO.Logic;
using PPO.Model;
using PPO.User_Interface;

namespace CLI
{
	public class NoteCLI
	{
		public class NoteCLI: INoteUI
		{
			private NotesService _notesService;

			public NoteCLI()
			{
				_notesService = new NotesService(new NotesFileRepo());
			}

			public void CreateNote()
			{
				Console.WriteLine("Создание новой заметки. Нажмите Esc для сохранения текста. " + "Введите содержимое заметки: ");
				string noteBody = "";
				ConsoleKey key = new();
				while (key != ConsoleKey.Escape)
				{
					key = Console.ReadKey().Key;
					noteBody += key;
				}
				Console.WriteLine("Удалить заметку через 24 часа (y/n): ");
				bool isTemporal = Console.ReadLine() == "y";

				Guid guid = Guid.NewGuid();
				_notesService.Create(new Note(guid, noteBody, isTemporal));
				DateTime noteCreationDateTime = DateTime.Now;
				Console.WriteLine("Заметка сохранена.\n\n" +
					$"Уникальный идентификатор: {guid}" +
					$"Время создания: {noteCreationDateTime}\n" +
					$"Текст\n: {noteBody}\n" +
					$"Автоудаление: {(isTemporal == true ? "да" : "нет")}\n"
				);
			}

			public void DeleteNote()
			{
				ShowNotes();
				Console.Write("============\n" + "Выберите идентификатор, по которому будет удалена заметки: ");
				Guid guid = Guid.Parse(Console.ReadLine());

				bool flag = false;
				foreach (var note in _notesService.GetNotesList("notes/*"))
					if (guid == note.Id)
					{
						_notesService.Delete(guid);
						flag = true;
					}

				if (flag == false)
					Console.WriteLine("Таких заметок не найдено.");
				else
					Console.WriteLine("Заметка удалена.");
			}

			public void EditNote()
			{
				ShowNotes();
				Console.Write("============\n" + "Выберите идентификатор, по которому будет удалена заметки: ");
				Guid guid = Guid.Parse(Console.ReadLine());

				bool flag = false;
				foreach (var note in _notesService.GetNotesList("notes/*"))
					if (guid == note.Id)
					{
						ChooseNoteParam(note);
						_notesService.Edit(guid);
						flag = true;
					}

				if (flag == false)
					Console.WriteLine("Таких заметок не найдено.");
				else
					Console.WriteLine("Заметка изменена.");
			}

			private void ChooseNoteParam(Note note)
			{
				int choice;
				do
				{
					Console.WriteLine("============\n" +
						"Выберите параметр, по которому будет изменена выбранная заметка:\n" +
						"0 - Готово\n" +
						"2 - Текст\n" +
						"3 - Цвет (на английском)\n" +
						$"4 - {(note.IsTemporal == true ? "Выключить автоудаления" : "Включить автоудаление")}"
					);
					try
					{
						choice = int.Parse(Console.ReadLine());
					}
					catch
					{
						choice = 0;
					}

					switch (choice)
					{
						case 1:
							Console.Write("Введите новое время для будильника (дд/ММ/гггг ЧЧ-мм-сс): ");
							alarmClock.AlarmTime = DateTime.Parse(Console.ReadLine());
							break;
						case 2:
							Console.Write("Введите новое название будильника: ");
							alarmClock.Name = Console.ReadLine();
							break;
						case 3:
							Console.Write("Введите новый цвет будильника: ");
							alarmClock.AlarmClockColor = Color.FromName(Console.ReadLine());
							break;
						case 4:
							alarmClock.IsWorking = !alarmClock.IsWorking;
							break;
					}
				} while (choice != 0);
			}

			public void Menu()
			{
				Console.WriteLine("\n0 - Выход из программы.\n" +
					"1 - Показать все будильники.\n" +
					"2 - Установить новый будильник.\n" +
					"3 - Изменить существующий будильник.\n" +
					"4 - Удалить существующий будильник.\n" +
					"\nВведите номер функции из списка:"
				);

				bool flag = false;
				int choice = -1;
				while (flag == false)
				{
					choice = int.Parse(Console.ReadLine());
					if (choice >= 0 && choice <= 4)
					{
						flag = true;
						break;
					}
					else
					{
						flag = false;
						Console.WriteLine("Ошибка ввода. Введите номер функции из списка.");
					}
				}

				switch (choice)
				{
					case 0:
						Exit();
						break;
					case 1:
						ShowAlarmClocks();
						Menu();
						break;
					case 2:
						CreatAlarmClock();
						Menu();
						break;
					case 3:
						EditAlarmClock();
						Menu();
						break;
					case 4:
						DeleteAlarmClock();
						Menu();
						break;
				}
			}

			public void ShowAlarmClocks()
			{
				Console.WriteLine("Список всех будильников\n" +
					"============"
				);
				foreach (var alarmclock in _alarmClockService.GetAlarmClocks("alarmclocks/*"))
					Console.WriteLine($"\nВремя: {alarmclock.AlarmTime}\n" +
						$"Название: {alarmclock.Name}\n" +
						$"Цвет: {alarmclock.AlarmClockColor}\n" +
						$"Режим: {(alarmclock.IsWorking == true ? "включён" : "выключен")}"
					);

				Console.WriteLine("============");
			}

			public void Exit()
			{
				Console.WriteLine("Выход из приложения...");
				Environment.Exit(0);
			}
		}
	}
}
