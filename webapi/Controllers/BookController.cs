using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Dtos.Book;
using webapi.Helpers;
using webapi.Initerfaces;
using webapi.Mappers;

namespace webapi.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController: ControllerBase
    {
        private readonly ApplicationDBcontext _context;
        private readonly IBookRespository _bookRepo;

        public BookController(ApplicationDBcontext context, IBookRespository book){
        _bookRepo=book;
        _context=context;
        
        }
        [Authorize(Roles ="User,Admin")]

        [HttpGet]
       
        public  async Task<IActionResult> Getall([FromQuery] QueryObjects queryObjects){
            var book= await _bookRepo.GetAllAsync(queryObjects);
            var bookDto=book.Select(s=>s.ToBookDto());
            return Ok(bookDto);
        }
        [HttpGet("{id:int}")]
        [Authorize(Roles ="User,Admin")]
       
        public async Task<IActionResult> Getbyid([FromRoute] int id){
            var bookmodel=await _bookRepo.Getbyid(id);
            return Ok(bookmodel.ToBookDto());
        }
        [HttpPost]
        [Authorize(Roles ="User,Admin")]

        public async Task<IActionResult> Create([FromBody] CreateBookDto createBookDto){
                    if (!ModelState.IsValid)return BadRequest(ModelState);

            var bookmodel= createBookDto.ToCreateBook();
            await _bookRepo.CreateAsync(bookmodel);
        return Ok(bookmodel.ToBookDto());
        //    return CreatedAtAction(nameof(Getbyid), new {id=bookmodel.Id},bookmodel.TobookDto());
        }
        [HttpPut]
        [Route("{id:int}")]
         [Authorize(Roles ="Admin")]

        public async Task<IActionResult> Update([FromRoute] int id ,[FromBody] UpdateBookDto updateDto){
                    if (!ModelState.IsValid)return BadRequest(ModelState);

            var bookmodel=await _bookRepo.UpdateAsync( id,updateDto);
            return Ok(bookmodel.ToBookDto());
        }
         [HttpDelete]
        [Route("{id:int}")]
         [Authorize(Roles ="Admin")]

        public async Task< IActionResult>  Delete([FromRoute] int id ){
            await _bookRepo.Delete(id);
            return NoContent();
        }
    }
}