using BookStoreApi.Models;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService) =>
            _booksService = booksService;

        [HttpGet]
        public async Task<List<Book>> GetAll() =>
            await _booksService.GetAll();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> GetById(string id)
        {
            var book = await _booksService.GetById(id);

            if (book is null)
                return NotFound();

            return book;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book newBook)
        {
            await _booksService.Create(newBook);

            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Book updatedBook)
        {
            var book = await _booksService.GetById(id);

            if (book is null)
                return NotFound();

            updatedBook.Id = book.Id;

            await _booksService.Update(id, updatedBook);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _booksService.GetById(id);

            if (book is null)
                return NotFound();

            await _booksService.Remove(id);

            return NoContent();
        }
    }
}
