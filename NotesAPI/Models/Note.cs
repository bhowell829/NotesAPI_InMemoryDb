using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesAPI.Models
{
    public class Note
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsComplete { get; set; }
    }
}
