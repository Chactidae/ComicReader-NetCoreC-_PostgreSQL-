using ComicReader.Context;
using ComicReader.Models;
using Microsoft.EntityFrameworkCore;

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
        public Book Save(Book book)
        {
            if (book.Id == 0) // Проверка на новую книгу
            {
                _context.Books.Add(book); // Добавление книги в контекст
            }
            else // Обновление существующей книги
            {
                _context.Entry(book).State = EntityState.Modified; // Установка состояния объекта на "Modified"
            }

            _context.SaveChanges(); // Сохранение изменений в базе данных

            return book; // Возвращение сохраненной книги
        }

    }
}
