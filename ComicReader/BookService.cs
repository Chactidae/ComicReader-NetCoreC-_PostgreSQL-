using ComicReader.Context;
using ComicReader.Controllers;
using ComicReader.Models;
using ComicReader.Repository;
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
                book.addImageToBook(previewImage);
               
                _context.Books.Add(book);
                _context.SaveChanges();
                // Получаем Id только что добавленного изображения
                book.PreviewImageId = book.Images.ElementAt(0).Id;
                _context.SaveChanges();
                //_logger.LogInformation("Получен запрос на поиск книги: {BookName}", );
            }
            return _context.Books.ToList();

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
        
        public Book getBook(long id) {  return _context.Books.SingleOrDefault(book => book.Id == id); }
    }
}
