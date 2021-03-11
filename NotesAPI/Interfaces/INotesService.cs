using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models;

namespace NotesAPI.Interfaces
{
    public interface INotesService
    {
        Task<ActionResult<IEnumerable<Note>>> GetNotes();
        Task<ActionResult<Note>> GetNote(long id);
        Task<IActionResult> PutNote(long id, Note note);
    }
}
