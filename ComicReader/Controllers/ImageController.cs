using ComicReader.Context;
using ComicReader.Models;
using ComicReader.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ComicReader.Controllers
{
    [ApiController]
    [Route("images")]
    public class ImageController : ControllerBase
    {
        private readonly ImageRepository _imageRepository;

        public ImageController(ImageRepository imageRepository, ApplicationDbContext context)
        {
            _imageRepository = imageRepository;
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(long id)
        {
            Image image = _imageRepository.GetImageById(id);

            if (image == null)
            {
                return NotFound(); // Возвращаем 404 Not Found, если изображение не найдено
            }

            var filePath = image.FilePath;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"File not found: {filePath}");
            }

            var imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(imageBytes, image.ContentType, image.FileNameImage);
        }
    }
}
