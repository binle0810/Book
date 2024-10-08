using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Dtos.Book
{
    public class UpdateBookDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Title cannot be over 100 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "Author name cannot be over 50 characters")]
        public string Author { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 1000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Published year must be between 1900 and 2100")]
        public int PublishedYear { get; set; }

        [MaxLength(30, ErrorMessage = "Genre cannot be over 30 characters")]
        public string Genre { get; set; } = string.Empty;


    }
}
