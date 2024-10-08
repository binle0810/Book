using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Favor
    {
        public string AppUserId { get; set; }
        public int BookId { get; set; }
        public AppUser AppUser { get; set; }
        public Book Book { get; set; }
    }
}