using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INT1448.Core.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [MaxLength(500)]
        public string Name { set; get; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime PubDate { get; set; }

        [Required]
        [Column(TypeName = "MONEY")]
        public int Cost { get; set; }

        [Required]
        [Column(TypeName = "MONEY")]
        public int Retail { get; set; }

        [Required]
        public int CategoryID { set; get; }
        
        [Required]
        public int PublisherID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual BookCategory BookCategory { get; set; }

        [NotMapped]
        [ForeignKey("PublisherID")]
        public virtual Publisher Publisher { get; set; }

        public virtual IEnumerable<BookAuthor> BookAuthors { get; set; }
    }
}