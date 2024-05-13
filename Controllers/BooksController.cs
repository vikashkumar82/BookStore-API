using BookStore_API.Model;
using BookStore_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository) 
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.getAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByID([FromRoute]int id)
        {
            var book = await _bookRepository.getBooksByIDAsync(id);
            return Ok(book);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] BookModel bookModel)
        {
            var id = await _bookRepository.addBookAsync(bookModel);
            return CreatedAtAction(nameof(GetBookByID), new {id=id, Controller = ("Books")},id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updatetBook([FromBody] BookModel bookModel, [FromRoute] int id)
        {
            await _bookRepository.updateBooksByIDAsync(id,bookModel);
            return Ok();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> updatetBookPatch([FromBody] JsonPatchDocument bookModel, [FromRoute] int id)
        {
            await _bookRepository.updateBooksByPatch(id, bookModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteBook([FromRoute] int id)
        {
            await _bookRepository.deleteBookAsync(id);
            return Ok();
        }
    }
}
