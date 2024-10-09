using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Dtos.Review;
using webapi.Extension;
using webapi.Initerfaces;
using webapi.Mappers;
using webapi.Models;
using webapi.Repository;

namespace webapi.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IBookRepository _bookrepo;
        private readonly UserManager<AppUser> _userManager;

        public ReviewController(IReviewRepository reviewRepo, IBookRepository bookrepo, UserManager<AppUser> userManager)
        {
            _reviewRepo = reviewRepo;
            _bookrepo = bookrepo;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]

        public async Task<IActionResult> GetAll()
        {
            var reviewmodel = await _reviewRepo.GetAllAsync();
            var reviewdto = reviewmodel.Select(x => x.ToReviewDto());
            return Ok(reviewdto);
        }
        [HttpGet("{id:int}")]
        [Authorize(Roles = "User,Admin")]


        public async Task<IActionResult> Getbyid([FromRoute] int id)
        {
            var reviewmodel = await _reviewRepo.Getbyid(id);
            if (reviewmodel ==null)return NotFound($"Review with ID {id} not found.");

            return Ok(reviewmodel.ToReviewDto());
        }

        [HttpPost("{bookid:int}")]
        [Authorize(Roles = "User,Admin")]

        public async Task<IActionResult> Create([FromRoute] int bookid, [FromBody] CreateReviewDto createReviewDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _bookrepo.BookExist(bookid))
            {
                return BadRequest($"Book does not exist in database {bookid} ");
            }
            var user= User.GetUsername();
            var appUser= await _userManager.FindByNameAsync(user);

            var reviewmodel = createReviewDto.ToCreateReviewDto(bookid);
            reviewmodel.AppUserId= appUser.Id;
            await _reviewRepo.CreateAsync(reviewmodel);
            return Ok(reviewmodel.ToReviewDto());
            //    return CreatedAtAction(nameof(Getbyid), new {id=bookmodel.Id},bookmodel.TobookDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewDto updateCmDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!await _reviewRepo.ReviewExist(id))
            {
                return BadRequest($"Review does not exist in database  ");
            }
            var reviewmodel = await _reviewRepo.UpdateAsync(id, updateCmDto);
            return Ok(reviewmodel.ToReviewDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _reviewRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}