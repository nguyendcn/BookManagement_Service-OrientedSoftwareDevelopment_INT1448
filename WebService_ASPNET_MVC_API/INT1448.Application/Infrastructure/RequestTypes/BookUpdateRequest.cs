using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Infrastructure.RequestTypes
{
    public class BookUpdateRequest
    {
        public int Id { get; set; }
        public string Name { set; get; }

        public DateTime PubDate { get; set; }

        public int Cost { get; set; }

        public int Retail { get; set; }

        public int CategoryID { set; get; }

        public int PublisherID { get; set; }

        public IEnumerable<string> BookImages { get; set; }

        public IEnumerable<int> BookAuthors { get; set; }
    }
}
