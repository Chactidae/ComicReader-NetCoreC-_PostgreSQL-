using ComicReader.Context;
using ComicReader.Models;

namespace ComicReader.Repository
{
    public class ImageRepository
    {
        private readonly ApplicationDbContext _context; // контекст базы данных

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Image GetImageById(long id)
        {
            return _context.Images.FirstOrDefault(x => x.Id == id);
        }


    }
}
