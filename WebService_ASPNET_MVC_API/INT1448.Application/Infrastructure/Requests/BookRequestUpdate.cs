using INT1448.Shared.CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Infrastructure.Requests
{
    public class BookRequestUpdate
    {
        public int Id { get;  set; }
        public string Name { set; get; }

        public string PubDate { get; set; }

        public int Cost { get; set; }

        public int Retail { get; set; }

        public int CategoryID { set; get; }

        public int PublisherID { get; set; }

        public ImageStatus ImageStatus { get; set; }
    }
}
