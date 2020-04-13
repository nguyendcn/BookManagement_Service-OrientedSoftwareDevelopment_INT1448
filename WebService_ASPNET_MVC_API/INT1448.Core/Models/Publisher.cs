using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INT1448.Core.Models
{
    [Table("Publishers")]
    public class Publisher
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
        [MaxLength(20)]
        public string Phone { set; get; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(500)]
        public string Address { set; get; }

        public virtual IEnumerable<Book> Books { set; get; }
    }
}