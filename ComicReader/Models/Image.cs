using System.ComponentModel.DataAnnotations.Schema;

namespace ComicReader.Models
{
    [Table("image")]
    public class Image
    {
        
        [Column("image_id")]
        public long Id { get; set; }
        
        [Column("name_image")]
        public string NameImage { get; set; }

        [Column("file_name")]
        public string FileNameImage { get; set; }

        [Column("size_image")]
        public long Size { get; set; }

        [Column("content_type")]
        public string ContentType { get; set; }

        [Column("is_preview")]
        public bool IsPreviewImage { get; set; }

        [Column("file_path")]
        public string FilePath { get; set; }

        [ForeignKey("book_book_id")]
        public virtual Book Book { get; set; }
    }
}
