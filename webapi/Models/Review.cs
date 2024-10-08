using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Models
{
    public class Review
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public int? BookId { get; set; }
    public Book? Book { get; set; }
    public string AppUserId{get;set;}
    public AppUser AppUser{get;set;}
}
}