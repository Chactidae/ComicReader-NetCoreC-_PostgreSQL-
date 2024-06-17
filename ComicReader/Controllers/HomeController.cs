using ComicReader.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Xml.Linq;

namespace ComicReader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookService _bookService;
        private List<Book> _books;

        [BindProperty]
        public Book Book { get; set; }
        public HomeController(ILogger<HomeController> logger, BookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }
        
        public IActionResult Index()
        {
            string b_name = Request.Query.FirstOrDefault(p => p.Key == "b_name").Value;
            _logger.LogInformation("Получен запрос на поиск книги: {BookName}", b_name);
            List<Book> books = _bookService.list(b_name);
            _books = books;
            return View(books);
        }
        public IActionResult BookInfo(long id)
        {
            Book currentBook = _bookService.getBook(id);
            return View(currentBook);
        }
        
        public async Task<IActionResult> CreateBook(Book book, IFormFile upload)
        {
           
            if (upload != null)
            {
                
                _bookService.addBook(book, upload);
                return View();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
