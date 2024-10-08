using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Models;

namespace webapi.Initerfaces
{
    public interface ITokenService
    {
    
        Task<string>  CreateToken(AppUser user);
    
    }
}