using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAPITests
{
    public class InMemoryItemsControllerTest : NotesControllerTest
    {
        public InMemoryItemsControllerTest()
            : base(
                new DbContextOptionsBuilder<NoteContext>()
                    .UseInMemoryDatabase("NoteList")
                    .Options)
        {
        }
    }
}
