using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Dtos.Book
{
    public class CreateBookDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Title cannot be over 100 characters")]
        [MinLength(1, ErrorMessage = "Title must be at least 1 character")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "Author name cannot be over 50 characters")]
        public string Author { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 999.99, ErrorMessage = "Price must be between 0.01 and 999.99")]
        public decimal Price { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Published year must be between 1900 and 2100")]
        public int PublishedYear { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Genre cannot be over 30 characters")]
        public string Genre { get; set; } = string.Empty;
    }
}
