using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.Data;
using webapi.Dtos.Review;
using webapi.Helpers;
using webapi.Initerfaces;
using webapi.Models;

namespace webapi.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDBcontext _context;

        public ReviewRepository(ApplicationDBcontext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync(QueryReviewObjects queryReview)
        {
            var reviewmodel=_context.Reviews.Include(x=>x.AppUser).AsQueryable();
            if(!queryReview.Title.IsNullOrEmpty())
            {
                reviewmodel=reviewmodel.Where(x=>x.Title.ToLower().Contains(queryReview.Title!.ToLower()));
            }
            if(queryReview.IsAscendingNewest){
             
                reviewmodel =reviewmodel.OrderByDescending(s=>s.CreatedOn);
            }
           return await reviewmodel.ToListAsync();
        }

        public async Task<Review?> Getbyid(int id)
        {
            var review=await _context.Reviews.Include(x=>x.AppUser).FirstOrDefaultAsync(x=>x.Id==id);
            if (review==null) {return null;}

             return review;

        }
           public async Task<Review> CreateAsync(Review reviewmodel)
        {
           await _context.AddAsync(reviewmodel);
           await _context.SaveChangesAsync();
            return reviewmodel;

        }
         public async Task<Review?> UpdateAsync(int id,UpdateReviewDto updateDto)
        {
            var reviewmodel=await _context.Reviews.FirstOrDefaultAsync(x=>x.Id==id);
            if (reviewmodel==null) {return null;}
            reviewmodel.Content=updateDto.Content;
            reviewmodel.Title=updateDto.Title;
            await _context.SaveChangesAsync();
            return reviewmodel;
        }

        public async Task<bool> ReviewExist(int id)
        {
     return await _context.Reviews.AnyAsync(x=>x.Id==id);
            
        }

        public async Task<Review?> DeleteAsync(int id)
        {
            var review=await _context.Reviews.FirstOrDefaultAsync(x=>x.Id==id);
            if (review==null){return null;};
            _context.Reviews.Remove(review);
           await _context.SaveChangesAsync();
            return review ;
        }
    }
}