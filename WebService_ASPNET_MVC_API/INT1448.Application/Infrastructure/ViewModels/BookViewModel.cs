using INT1448.Application.Infrastructure.DTOs;
using System;
using System.Collections.Generic;

namespace INT1448.Application.Infrastructure.ViewModels
{
    public class BookViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public DateTime PubDate { get; set; }

        public int Cost { get; set; }

        public int Retail { get; set; }

        public string BookCategoryName { set; get; }

        public string PublisherName { get; set; }

        public IEnumerable<BookImageDTO> BookImages { get; set; }
    }
}
