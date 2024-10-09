using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webapi.Extension;
using webapi.Initerfaces;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/favor")]
    [ApiController]
    public class favorController: ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBookRepository _ibookrespo;
        private readonly IFavorRepository _ifavorrepo;

        public favorController(UserManager<AppUser> userManager, IBookRepository ibookrespo, IFavorRepository ifavorrepo)
        {
            _userManager = userManager;
            _ibookrespo = ibookrespo;
            _ifavorrepo = ifavorrepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetuserFavor(){
            var username= User.GetUsername();
            var appUser= await _userManager.FindByNameAsync(username);
            var userfavor=await _ifavorrepo.GetFavor(appUser);
            return Ok(userfavor);
        }
          [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddFavor( string Title)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var book = await _ibookrespo.GetBooktitle(Title);

         

            if (book == null) return BadRequest("Book not found");

            var userPortfolio = await _ifavorrepo.GetFavor(appUser);

            if (userPortfolio.Any(e => e.Title.ToLower() == Title.ToLower())) return BadRequest("Cannot add same book to favor");

            var favor = new Favor
            {
                BookId = book.Id,
                AppUserId = appUser.Id
            };

            await _ifavorrepo.CreateFavorasync(favor);

            if (favor == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Ok(favor);
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Deletefavor( string Title)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userfavor = await _ifavorrepo.GetFavor(appUser);

            var filteredBook = userfavor.Where(s => s.Title.ToLower() == Title.ToLower()).ToList();

            if (filteredBook.Count > 0)
            {
                await _ifavorrepo.DeleteFavor(appUser, Title);
            }
            else
            {
                return BadRequest("Book not in your Favor");
            }

            return Ok();
        }
    }
}