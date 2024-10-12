using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Dtos.Book;
using webapi.Helpers;
using webapi.Initerfaces;
using webapi.Mappers;
using webapi.Models;

namespace webapi.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDBcontext _context;
    

        public BookRepository(ApplicationDBcontext context){

        _context=context;
        
        }

        public async Task<Book> CreateAsync(Book bookmodel)
        {
           await _context.AddAsync(bookmodel);
           await _context.SaveChangesAsync();
            return bookmodel;

        }

        public async Task<Book?> Delete(int id)
        {
           var bookmodel=await _context.Books.FirstOrDefaultAsync(x=>x.Id==id);
            if (bookmodel==null) {return null;}

           _context.Books.Remove(bookmodel);
           await _context.SaveChangesAsync();
           return bookmodel;
        }

        public async Task<List<Book>> GetAllAsync(QueryBookObjects queryObjects)
        {
            var books= _context.Books.Include(x=>x.Reviews).ThenInclude(a=>a.AppUser).AsQueryable();
            if(!string.IsNullOrWhiteSpace(queryObjects.Author)){
                books=books.Where(x=>x.Author.ToLower().Contains(queryObjects.Author.ToLower()));
            }
             if(!string.IsNullOrWhiteSpace(queryObjects.Title)){
                books=books.Where(x=>x.Title.ToLower().Contains(queryObjects.Title.ToLower()));
            }
               if (!string.IsNullOrWhiteSpace(queryObjects.SortBy))
            {
                if (queryObjects.SortBy.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    books = queryObjects.IsAscending ? books.OrderBy(s => s.Author) : books.OrderByDescending(s => s.Author);
                }
            }
            var skipnumber=(queryObjects.PageNumber-1)*queryObjects.PageSize;
            return await books.Skip(skipnumber).Take(queryObjects.PageSize).ToListAsync();
        }

        public async Task<Book?> Getbyid(int id)
        {
           var bookmodel= await _context.Books.Include(x=>x.Reviews).ThenInclude(y=>y.AppUser).FirstOrDefaultAsync(x=>x.Id==id);
            if (bookmodel==null) {return null;}

            return bookmodel;
        }

        public async Task<bool> BookExist(int id)
        {
            return await _context.Books.AnyAsync(x=>x.Id==id);
            
        }

        public async Task<Book?> UpdateAsync(int id,UpdateBookDto updateDto)
        {
            var bookmodel=await _context.Books.FirstOrDefaultAsync(x=>x.Id==id);
            if (bookmodel==null) {return null;}
              bookmodel.Author=updateDto.Author;
            bookmodel.Title=updateDto.Title;
            bookmodel.Price=updateDto.Price;
            bookmodel.PublishedYear=updateDto.PublishedYear;
            bookmodel.Genre=updateDto.Genre;
            await _context.SaveChangesAsync();
            return bookmodel;
        }

        
        public async Task<Book?> GetBooktitle(string Title)
        {
            var book=await _context.Books.FirstOrDefaultAsync(x=>x.Title.ToLower()==Title.ToLower());
            return book;
        }
    }
}