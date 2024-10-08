using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class ApplicationDBcontext:IdentityDbContext<AppUser>
    {
        public ApplicationDBcontext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Book> Books{get;set;}    
        public DbSet<Review>Reviews{get;set;}
        public DbSet<Favor>Favors{get;set;}

         protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           
            builder.Entity<Favor>(x => x.HasKey(p => new { p.AppUserId, p.BookId }));

            builder.Entity<Favor>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Favors)
                .HasForeignKey(p => p.AppUserId);

            builder.Entity<Favor>()
                .HasOne(u => u.Book)
                .WithMany(u => u.Favors)
                .HasForeignKey(p => p.BookId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}