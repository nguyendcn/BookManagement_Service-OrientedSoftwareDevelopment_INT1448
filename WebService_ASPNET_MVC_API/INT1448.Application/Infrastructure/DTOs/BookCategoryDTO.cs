using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Infrastructure.DTOs
{
    public class BookCategoryDTO
    {
        public int ID { get; set; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public string Description { set; get; }
    }
}
