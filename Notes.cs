using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes {
	internal class Note {
		private protected int _number;
		public int Number {
			get {return _number;}
			//	private set {
			//		if (value >= 0 && !_checklist.Contains(value)) {
			//			_number = value;
			//			_checklist.Add(value);
			//		} else {
			//			Console.WriteLine("Такой идентификатор уже существует.");
			//		}
			//}
		}

		private protected DateTime _creationTime;
		private protected string _body;
		private protected bool _isTemporal;

		public Note() {}
		public Note(int number, string body, bool isTemporal) {
			_number = number;
			_creationTime = DateTime.Now;
			_body = body;
			_isTemporal = isTemporal;
		}

		TimeSpan RemovalTime(DateTime currentDayTime) {
			return DateTime.Now - _creationTime;
		}
	}

	internal class NotesService: Note {
		private protected static SortedSet<Note> _checklist = new(); // в INotesRepo

		public Note Create(int number, string body, bool isTemporal) {
			foreach (Note elem in _checklist)
				if (elem.Number == number)
					throw new Exception("Такой идентификатор уже существует.");

			return new Note(
				_number = number,
				_body = body,
				_isTemporal = isTemporal
			);
		}

		public void Edit(int number, string body, bool isTemporal) {
			_number = number;
			_body = body;
			_isTemporal = isTemporal;
		}

		public void Delete(int number) {
			foreach (Note elem in _checklist)
				if (elem.Number == number)
					_checklist.Remove(elem);
			Console.WriteLine("Идентификатор удалён.");
		}
	}
}
