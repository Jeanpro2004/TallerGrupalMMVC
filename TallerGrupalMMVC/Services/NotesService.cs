using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TallerGrupalMMVC.Models;

namespace TallerGrupalMMVC.Services
{
    public class NotesService
    {
        public async Task<List<Note>> GetAllNotesAsync()
        {
            var notes = new List<Note>();

            var notesDir = Path.Combine(FileSystem.AppDataDirectory, "Notes");
            Directory.CreateDirectory(notesDir);

            var files = Directory.GetFiles(notesDir, "*.txt");

            foreach (var file in files)
            {
                try
                {
                    var text = await File.ReadAllTextAsync(file);
                    var fileInfo = new FileInfo(file);

                    notes.Add(new Note
                    {
                        Filename = Path.GetFileName(file),
                        Text = text,
                        Date = fileInfo.LastWriteTime
                    });
                }
                catch (Exception ex)
                {
                    // Log error if needed
                }
            }

            return notes.OrderByDescending(n => n.Date).ToList();
        }

        public async Task SaveNoteAsync(Note note)
        {
            var notesDir = Path.Combine(FileSystem.AppDataDirectory, "Notes");
            Directory.CreateDirectory(notesDir);

            if (string.IsNullOrEmpty(note.Filename))
            {
                note.Filename = $"note_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            }

            var filePath = Path.Combine(notesDir, note.Filename);
            await File.WriteAllTextAsync(filePath, note.Text);

            note.Date = DateTime.Now;
        }

        public async Task DeleteNoteAsync(Note note)
        {
            var notesDir = Path.Combine(FileSystem.AppDataDirectory, "Notes");
            var filePath = Path.Combine(notesDir, note.Filename);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}