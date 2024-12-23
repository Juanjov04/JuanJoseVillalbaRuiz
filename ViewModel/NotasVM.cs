using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace JuanJoseVillalbaRuiz.ViewModel
{
    public class NotasViewModel : BindableObject
    {
        private readonly NotesDatabase _database;
        private string _title;
        private string _content;

        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveNoteCommand { get; }

        public NotasViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notas.db3");
            _database = new NotesDatabase(dbPath);

            SaveNoteCommand = new Command(async () => await SaveNoteAsync());
            LoadNotesAsync();
        }

        private async void LoadNotesAsync()
        {
            var notes = await _database.GetNotesAsync();
            Notes.Clear();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }

        private async Task SaveNoteAsync()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Content))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var newNote = new Note
            {
                Title = Title,
                Content = Content,
                CreatedAt = DateTime.Now
            };

            await _database.SaveNoteAsync(newNote);

            Title = string.Empty;
            Content = string.Empty;

            LoadNotesAsync();
        }
    }
}
