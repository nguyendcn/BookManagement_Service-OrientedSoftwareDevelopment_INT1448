using INT1448.Application.Infrastructure.DTOs;
using INT1448.Shared.CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Infrastructure.Requests
{
    public class BookInfoRequestMessage
    {
        public string Name { set; get; }

        public int Cost { get; set; }

        public int Retail { get; set; }

        public int CategoryID { set; get; }

        public int PublisherID { get; set; }
        public ImageStatus ImageStatus { get; set; }
    }
}
