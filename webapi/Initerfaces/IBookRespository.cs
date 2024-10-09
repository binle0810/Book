using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Dtos.Book;
using webapi.Helpers;
using webapi.Models;

namespace webapi.Initerfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(QueryObjects queryObjects);
        Task<Book> CreateAsync(Book book);
        Task<Book?> Getbyid(int id);
        Task<Book?> UpdateAsync(int id,UpdateBookDto updateDto);
        Task<Book?> Delete(int id);
        Task<bool> BookExist(int id);
        Task<Book?> GetBooktitle(string Title);

    }
}