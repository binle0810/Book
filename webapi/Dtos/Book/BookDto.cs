using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Dtos.Review;
using webapi.Dtos.Review;
using webapi.Models;

namespace webapi.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int PublishedYear { get; set; }
        public string Genre { get; set; } = string.Empty;
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    }
}