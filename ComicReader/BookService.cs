using ComicReader.Context;
using ComicReader.Models;
using ComicReader.Repository;

namespace ComicReader
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ApplicationDbContext _context;
        public BookService(ApplicationDbContext context) 
        {
            _context = context;
        }

        private readonly String folderPath = "C:\\ComicData\\";
       


        public List<Book> list(string b_name)
        {
            if (b_name == "")
            {
                return _context.Books.ToList();
            }
            else
            {
                return _context.Books.Where(book=>book.Title == b_name).ToList();
            }
           
        }
        public List<Book> addBook(Book book) 
        {
            if (book != null)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            return _context.Books.ToList();

        }
    }
}
