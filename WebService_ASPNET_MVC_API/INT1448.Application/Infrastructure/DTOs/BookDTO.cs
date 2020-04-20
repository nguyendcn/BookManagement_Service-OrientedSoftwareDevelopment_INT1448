using System;
using System.Collections.Generic;

namespace INT1448.Application.Infrastructure.DTOs
{
    public class BookDTO
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public DateTime PubDate { get; set; }

        public int Cost { get; set; }

        public int Retail { get; set; }

        public int CategoryID { set; get; }

        public int PublisherID { get; set; }

        public IEnumerable<BookImageDTO> BookImages { get; set; }

        public IEnumerable<BookAuthorDTO> BookAuthors { get; set; }
    }
}
