using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Initerfaces
{
    public interface IFavorRepository
    {
        Task<List<Book>> GetFavor(AppUser appUser);
        Task<Favor> CreateFavorasync( Favor favor);
          Task<Favor> DeleteFavor(AppUser appUser, string Title);
    }
}