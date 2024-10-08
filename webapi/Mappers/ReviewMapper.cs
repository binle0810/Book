using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.Dtos.Review;
using webapi.Models;

namespace webapi.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDto ToReviewDto(this Review review)
        {
            return new ReviewDto{
                Id=review.Id,
                Title = review.Title,
                Content = review.Content,
                CreatedOn = review.CreatedOn,
                CreateBy=review.AppUser.UserName,
                BookId = review.BookId,
             
            };

    }
    public static Review ToCreateReviewDto(this CreateReviewDto review,int bookid)
        {
            return new Review{
           
                Title = review.Title,
                Content = review.Content,
                CreatedOn = DateTime.Now,
                BookId = bookid,
             
            };

    }
    }
}