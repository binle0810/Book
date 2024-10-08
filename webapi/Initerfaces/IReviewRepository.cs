using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Dtos.Review;
using webapi.Models;

namespace webapi.Initerfaces
{
    public interface IReviewRepository
    {
     
         Task<List<Review>> GetAllAsync();
           Task<Review?> Getbyid(int id);
           Task<Review> CreateAsync(Review review);
           Task<Review?> UpdateAsync(int id,UpdateReviewDto updateDto);
        Task<Review?>  DeleteAsync(int id);
           
        Task<bool> ReviewExist(int id);

    }
}