using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Core.Models
{
    [Table("BookImages")]
    public class BookImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImagePath { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
