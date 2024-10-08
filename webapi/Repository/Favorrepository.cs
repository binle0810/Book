using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Initerfaces;
using webapi.Mappers;
using webapi.Models;

namespace webapi.Repository
{
    public class Favorrepository : IFavorRepository
    {
        private readonly ApplicationDBcontext _context;

        public Favorrepository(ApplicationDBcontext context)
        {
            _context = context;
        }

        public async Task<Favor> CreateFavorasync(Favor favor)
        {
           await _context.Favors.AddAsync(favor);
           await _context.SaveChangesAsync();
            return favor;
        }

        public async Task<List<Book>> GetFavor(AppUser appUser)
        {
            return await _context.Favors.Include(x=>x.Book.Reviews).Where(u=>u.AppUserId==appUser.Id)
            .Select(book =>new Book{
            Id = book.BookId,
            Title = book.Book.Title,
            Author = book.Book.Author,
            Price = book.Book.Price,
            PublishedYear = book.Book.PublishedYear,
            Genre = book.Book.Genre,
            Reviews = book.Book.Reviews.ToList()
            }
            ).ToListAsync();
        }
          public async Task<Favor> DeleteFavor(AppUser appUser, string Title)
        {
            var favorModel = await _context.Favors.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Book.Title.ToLower() == Title.ToLower());

            if (favorModel == null)
            {
                return null;
            }

            _context.Favors.Remove(favorModel);
            await _context.SaveChangesAsync();
            return favorModel;
        }
    }
}