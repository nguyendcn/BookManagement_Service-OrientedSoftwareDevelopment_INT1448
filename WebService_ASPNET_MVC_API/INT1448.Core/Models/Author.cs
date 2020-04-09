using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INT1448.Core.Models
{
    [Table("Authors")]
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(256)]
        public string FullName { set; get; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(500)]
        public string Address { set; get; }

        public virtual IEnumerable<BookAuthor> BookAuthors { get; set; }
    }
}