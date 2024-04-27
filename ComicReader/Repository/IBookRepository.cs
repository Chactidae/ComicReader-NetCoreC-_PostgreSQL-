using ComicReader.Models;

namespace ComicReader.Repository
{
    public interface IBookRepository
    {
        List<Book> GetAllBooks();
    }
}
