using ComicReader.Context;
using ComicReader.Models;

namespace ComicReader.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
    
    }
}
