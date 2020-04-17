using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Infrastructure.Requests
{
    public class BookRequestCreate
    {
        public string Name { set; get; }

        public int Cost { get; set; }

        public int Retail { get; set; }

        public int CategoryID { set; get; }

        public int PublisherID { get; set; }
    }
}
