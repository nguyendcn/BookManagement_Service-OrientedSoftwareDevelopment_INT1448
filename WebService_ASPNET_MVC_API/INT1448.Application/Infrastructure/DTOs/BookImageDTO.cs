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
    }
}
