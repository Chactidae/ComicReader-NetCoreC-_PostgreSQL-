
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicReader.Models
{
    [Table("book")]
    public class Book
    {
        public Book()
        {

        }
        [Column("book_id")]
        public long Id { get; set; }

        [Column("b_name")]
        public string Title { get; set; }
        [Column("cover")]
        public string Cover { get; set; }
        [Column("author_id")]
        public int AuthorId { get; set; }
        [Column("book_info_id")]
        public int BookInfoId { get; set; }

        [Column("preview_image_id")]
        public long PreviewImageId { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();
        

        public void addImageToBook(Image image)
        {
            image.Book = this;
            Images.Add(image);
        }

        public override bool Equals(object? obj)
        {
            return obj is Book book &&
                   Id == book.Id &&
                   Title == book.Title &&
                   Cover == book.Cover &&
                   AuthorId == book.AuthorId &&
                   BookInfoId == book.BookInfoId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Cover, AuthorId, BookInfoId);
        }

        public String toString()
        {
            return "Book(id=" + this.Id + ", title=" + this.Title + ", cover=" + this.Cover + ", author=" + this.AuthorId + ", book_info_id=" + this.BookInfoId + ")";
        }
    }
}
