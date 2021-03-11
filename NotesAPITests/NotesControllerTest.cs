using System;
using Xunit;
using NotesAPI.Controllers;
using NotesAPI.Interfaces;
using NotesAPI.Services;
using NotesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NotesAPITests
{
    public abstract class NotesControllerTest
    {
        protected DbContextOptions<NoteContext> ContextOptions { get; }

        protected NotesControllerTest(DbContextOptions<NoteContext> contextOptions)
        {
            ContextOptions = contextOptions;

            AddDataToMemory();
        }

        private void AddDataToMemory()
        {
            using (var context = new NoteContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var note = new List<Note>()
                {
                    new Note() { Id = 1, Title = "Dog", Body="Afternoon walk", IsComplete = true },
                    new Note() { Id = 2, Title = "School", Body="Do homework", IsComplete = true },
                    new Note() { Id = 3, Title = "Errands", Body="Grocery shop", IsComplete = false },
                    new Note() { Id = 4, Title = "Work", Body="Code new app", IsComplete = false }
                };

                context.AddRange(note);
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task GetNotes()
        {
            using (var context = new NoteContext(ContextOptions))
            {
                var controller = new NotesController(context);
                var response = await controller.GetNotes();
                var notes = response.Value;

                Assert.IsType<ActionResult<IEnumerable<Note>>>(response);
                Assert.Equal(4, notes.Count());
            }
        }

        [Fact]
        public async Task GetNote()
        {
            using (var context = new NoteContext(ContextOptions))
            {
                var controller = new NotesController(context);
                var response = await controller.GetNote(1);
                var note = response.Value;

                Assert.Equal(1, note.Id);
            }
        }

        [Fact]
        public async Task PutNote()
        {
            using (var context = new NoteContext(ContextOptions))
            {
                var controller = new NotesController(context);
                var newNote = new Note() { Id = 2, Title = "Hobby", Body = "Play hockey", IsComplete = true };
                var response = await controller.PutNote(2, newNote);
                var note = context.Set<Note>().Single(e => e.Id == 2);

                Assert.Equal("Hobby", note.Title);
                Assert.Equal("Play hockey", note.Body);
            }
        }

        [Fact]
        public async Task PostNote()
        {
            using (var context = new NoteContext(ContextOptions))
            {
                var controller = new NotesController(context);
                var newNote = new Note() { Id = 5, Title = "Hobby", Body = "Play hockey", IsComplete = true };
                var response = await controller.PostNote(newNote);
                var note = context.Set<Note>().Single(e => e.Id == 5);

                Assert.Equal(5, note.Id);
            }
        }

        [Fact]
        public async Task DeleteNote()
        {
            using (var context = new NoteContext(ContextOptions))
            {
                var controller = new NotesController(context);
                var response = await controller.DeleteNote(1);

                Assert.False(context.Set<Note>().Any(e => e.Id == 1));
            }
        }
    }
}
