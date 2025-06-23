using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TallerGrupalMMVC.Models;
using TallerGrupalMMVC.Services;

namespace TallerGrupalMMVC.ViewModels
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        private readonly NotesService _notesService;
        private bool _isLoading = false;
        private Note _selectedNote;

        public ObservableCollection<Note> Notes { get; set; } = new();

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                if (_selectedNote != value)
                {
                    _selectedNote = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoadNotesCommand { get; }
        public ICommand AddNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand SaveNoteCommand { get; }

        public NotesViewModel()
        {
            _notesService = new NotesService();

            LoadNotesCommand = new Command(async () => await LoadNotesAsync());
            AddNoteCommand = new Command(AddNote);
            DeleteNoteCommand = new Command<Note>(async (note) => await DeleteNoteAsync(note));
            SaveNoteCommand = new Command<Note>(async (note) => await SaveNoteAsync(note));

            _ = LoadNotesAsync();
        }

        private async Task LoadNotesAsync()
        {
            try
            {
                IsLoading = true;
                var notes = await _notesService.GetAllNotesAsync();

                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    $"Error loading notes: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void AddNote()
        {
            var newNote = new Note { Text = "Nueva nota..." };
            Notes.Insert(0, newNote);
            SelectedNote = newNote;
        }

        private async Task DeleteNoteAsync(Note note)
        {
            if (note == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirmar",
                "¿Estás seguro de que quieres eliminar esta nota?",
                "Sí", "No");

            if (confirm)
            {
                try
                {
                    await _notesService.DeleteNoteAsync(note);
                    Notes.Remove(note);
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error",
                        $"Error deleting note: {ex.Message}", "OK");
                }
            }
        }

        private async Task SaveNoteAsync(Note note)
        {
            if (note == null || string.IsNullOrWhiteSpace(note.Text)) return;

            try
            {
                await _notesService.SaveNoteAsync(note);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Nota guardada correctamente", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    $"Error saving note: {ex.Message}", "OK");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}