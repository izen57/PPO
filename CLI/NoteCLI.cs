using RepositoriesImplementations;
using Logic;
using Model;

using User_Interface;

namespace CLI
{
	public class NoteCLI: INoteUI
	{
		private INoteService _noteService;

		public NoteCLI()
		{
			_noteService = new NoteService(new NoteFileRepo());
		}

		public void CreateNote()
		{
			Console.WriteLine("Создание новой заметки. Нажмите Esc для сохранения текста. " + "Введите содержимое заметки: ");
			string noteBody = "";
			ConsoleKeyInfo key = new();
			while (key.Key != ConsoleKey.Escape)
			{
				key = Console.ReadKey();
				noteBody += key.KeyChar;
			}
			Console.Write("\nУдалить заметку через 24 часа (y/n): ");
			bool isTemporal = Console.ReadLine() == "y";

			Guid guid = Guid.NewGuid();
			try
			{
				_noteService.Create(new Note(guid, noteBody, isTemporal));
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Environment.Exit(1);
			}

			DateTime noteCreationDateTime = DateTime.Now;
			Console.WriteLine("\nЗаметка сохранена.\n\n" +
				$"Уникальный идентификатор: {guid}" +
				$"Время создания: {noteCreationDateTime}\n" +
				$"Текст\n: {noteBody}\n" +
				$"Автоудаление: {(isTemporal == true ? "да" : "нет")}\n"
			);
		}

		public void DeleteNote()
		{
			ShowNotes();
			Console.Write("============\n" + "Выберите идентификатор, по которому будет удалена заметка: ");
			Guid guid = Guid.Parse(Console.ReadLine());

			bool flag = false;
			foreach (var note in _noteService.GetNotesList("notes/*"))
				if (guid == note.Id)
				{
					try
					{
						_noteService.Delete(guid);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						Environment.Exit(1);
					}

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
			Console.Write("============\n" + "Выберите идентификатор, по которому будет удалена заметка: ");
			Guid guid = Guid.Parse(Console.ReadLine());

			bool flag = false;
			foreach (var note in _noteService.GetNotesList("notes/*"))
				if (guid == note.Id)
				{
					ChooseNoteParam(note);
					try
					{
						_noteService.Edit(note);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
						Environment.Exit(1);
					}
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
					"1 - Текст\n" +
					$"2 - {(note.IsTemporal == true ? "Выключить автоудаление" : "Включить автоудаление")}"
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
						Console.WriteLine("Старый текст заметки:\n" + note.Body);
						Console.WriteLine("Новый текст заметки (нажмите Esc для сохранения текста):");
						note.Body = "";

						ConsoleKeyInfo key = new();
						while (key.Key != ConsoleKey.Escape)
						{
							key = Console.ReadKey();
							note.Body += key.KeyChar;
						}
						break;
					case 2:
						note.IsTemporal = !note.IsTemporal;
						Console.WriteLine($"Автоудаление {(note.IsTemporal == true ? "включено" : "выключено")}");
						break;
				}
			} while (choice != 0);
		}

		public void Menu()
		{
			Console.WriteLine("\n0 - Выход из программы.\n" +
				"1 - Показать все заметки.\n" +
				"2 - Создать новую заметку.\n" +
				"3 - Редактировать заметку.\n" +
				"4 - Удалить заметку.\n" +
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
					ShowNotes();
					Menu();
					break;
				case 2:
					CreateNote();
					Menu();
					break;
				case 3:
					EditNote();
					Menu();
					break;
				case 4:
					DeleteNote();
					Menu();
					break;
			}
		}

		public void ShowNotes()
		{
			Console.WriteLine("Список всех заметок\n============");
			try
			{
				foreach (var note in _noteService.GetNotesList("notes/*"))
					Console.WriteLine($"\nУникальный идентификатор: {note.Id}\n" +
						$"Дата и время создания: {note.CreationTime}\n" +
						$"Текст заметки: {note.Body}\n" +
						$"Автоудаление: {(note.IsTemporal == true ? "включено" : "выключено")}"
					);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Environment.Exit(1);
			}


			Console.WriteLine("============");
		}

		public void Exit()
		{
			Console.WriteLine("Выход из приложения...");
			Environment.Exit(0);
		}
	}
}
