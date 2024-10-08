using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Dtos.Book;
using webapi.Models;

namespace webapi.Mappers
{
    public static class BookMapper
    {
        public static BookDto ToBookDto(this Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre,
                Reviews = book.Reviews.Select(c => c.ToReviewDto()).ToList()
            };
        }

        public static Book ToCreateBook(this CreateBookDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Price = bookDto.Price,
                PublishedYear = bookDto.PublishedYear,
                Genre = bookDto.Genre,
            };
        }

        public static Book ToUpdateBook(this UpdateBookDto bookDto, Book existingBook)
        {
            existingBook.Title = bookDto.Title;
            existingBook.Author = bookDto.Author;
            existingBook.Price = bookDto.Price;
            existingBook.PublishedYear = bookDto.PublishedYear;
            existingBook.Genre = bookDto.Genre;

            return existingBook;
        }
    }
}
