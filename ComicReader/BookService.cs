using ComicReader.Context;
using ComicReader.Controllers;
using ComicReader.Models;
using ComicReader.Repository;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Xml.Linq;

namespace ComicReader
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        public BookService(ApplicationDbContext context) 
        {
            _context = context;
        }

        private readonly String folderPath = "C:\\ComicData\\";
       


        public List<Book> list(string b_name)
        {
            if (b_name == "" || b_name == null)
            {
                return _context.Books.ToList();
            }
            else
            {
                return _context.Books.Where(book=>book.Title == b_name).ToList();
            }
           
        }
        public List<Book> addBook(Book book, IFormFile upload) 
        {
            if (book != null && upload != null)
            {
                Image previewImage = toImageEntity(upload);
                previewImage.IsPreviewImage = true;

                // Добавляем книгу в контекст ПЕРЕД вызовом addImageToBook
                _context.Books.Add(book);

                book.addImageToBook(previewImage);

                _context.SaveChanges(); // Сохраняем как книгу, так и изображение

                book.PreviewImageId = previewImage.Id; // Используйте previewImage.Id напрямую

                _context.SaveChanges();
            }
            return _context.Books.ToList();

        }
        public Book addPageToBook(Book book, IFormFile imagePage)
        {
            if (imagePage != null)
            {
                Image newPage = toImageEntity(imagePage);

                // Устанавливаем связь с книгой
                newPage.Book = book;

                // Добавляем изображение в контекст
                _context.Images.Add(newPage);

                _context.SaveChanges();
            }
            return book;
        }
        private Image toImageEntity(IFormFile file)
        {
            Image image = new Image();
            image.NameImage = file.Name;
            image.FileNameImage = file.FileName;
            image.ContentType = file.ContentType;
            image.Size = file.Length;
            image.FilePath = (folderPath + file.FileName);
            if (file != null && file.Length > 0)
            {
                string filePath = Path.Combine(folderPath, file.FileName);

                // Создание файла с помощью FileStream
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Копирование файла в файл-поток
                    file.CopyTo(fileStream);
                }

                Console.WriteLine("Успешно загружено");
            }
            return image;
        }

        public Book getBook(long id)
        {
            return _context.Books
                           .Include(b => b.Images) // Загружаем Images 
                           .Single(book => book.Id == id);
        }

    }
}
