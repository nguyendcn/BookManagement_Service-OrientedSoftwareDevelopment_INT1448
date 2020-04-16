using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INT1448.Core.Models
{
    [Table("BookCategories")]
    public class BookCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(500)]
        public string Name { set; get; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(500)]
        public string Alias { set; get; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(500)]
        public string Description { set; get; }

        public virtual ICollection<Book> Books { set; get; }
    }
}