using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
      
    public class Book
    {
       public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
   
    public decimal Price { get; set; }
    public int PublishedYear { get; set; }
    public string Genre { get; set; } = string.Empty;

    public List<Review> Reviews { get; set; } = new List<Review>();
        public List<Favor> Favors { get; set; }= new List<Favor>();

    }
}