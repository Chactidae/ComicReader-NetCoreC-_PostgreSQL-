using System.Drawing;

namespace ComicReader.Repository
{
    public interface IImageRepository
    {
        // Метод для получения изображения по ID
        Image GetImageById(long id);
    }
}
