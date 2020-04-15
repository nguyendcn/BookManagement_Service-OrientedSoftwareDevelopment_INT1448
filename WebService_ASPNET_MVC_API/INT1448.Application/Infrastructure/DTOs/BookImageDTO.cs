using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Infrastructure.DTOs
{
    public class BookImageDTO
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string ImagePath { get; set; }

        public string Caption { get; set; }

        public bool IsDefault { get; set; }

        public DateTime DateCreated { get; set; }

        public int SortOrder { get; set; }

        public long FileSize { get; set; }
    }
}
