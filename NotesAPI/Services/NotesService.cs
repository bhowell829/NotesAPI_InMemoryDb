using NotesAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace NotesAPI.Services
{
    public class NotesService : INotesService
    {
        private readonly List<Note> _note;
        public NotesService()
        {
            _note = new List<Note>()
            {
                new Note() { Id = 1, Title = "Dog", Body="Afternoon walk", IsComplete = true },
                new Note() { Id = 2, Title = "School", Body="Do homework", IsComplete = true },
                new Note() { Id = 3, Title = "Errands", Body="Grocery shop", IsComplete = false },
                new Note() { Id = 4, Title = "Work", Body="Code new app", IsComplete = false }
            };
        }
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            //return _note;
            return await Task.Run(() => _note);
        }
        public async Task<ActionResult<Note>> GetNote(long id)
        {
            return await Task.Run(() => _note.Where(a => a.Id == id).FirstOrDefault());
        }
        public async Task<IActionResult> PutNote(long id, Note note)
        {
            note.Id = 5;
            _note.Add(note);
            return (IActionResult)await Task.Run(() => _note.Where(a => a.Id == id).FirstOrDefault());
        }
    }
}
