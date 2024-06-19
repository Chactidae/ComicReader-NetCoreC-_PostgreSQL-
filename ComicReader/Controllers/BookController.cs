using ComicReader.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComicReader.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly BookService _bookService;

        public BookController(ILogger<BookController> logger, BookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }
        public IActionResult BookInfo(long id)
        {
            Book currentBook = _bookService.getBook(id);
            return View(currentBook);
        }
        public IActionResult AddPages(long id, IFormFile imagePage)
        {
            Book currentBook = _bookService.getBook(id);
            if (imagePage != null)
            {
                _bookService.addPageToBook(currentBook, imagePage);

            }
            return View(currentBook);
        }
    }
}
