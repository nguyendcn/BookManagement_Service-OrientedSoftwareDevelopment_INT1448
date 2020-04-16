using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Core.ViewModels
{
    public class BookViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public DateTime PubDate { get; set; }

        public int Cost { get; set; }

        public int Retail { get; set; }

        public string CategoryName { set; get; }

        public string PublisherName { get; set; }

        public IEnumerable<string> Images { get; set; }
    }
}
