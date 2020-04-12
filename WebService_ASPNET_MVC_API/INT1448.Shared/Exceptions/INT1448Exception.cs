using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Shared.Exceptions
{
    public class INT1448Exception : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public INT1448Exception() : base()
        {
            this.StatusCode = HttpStatusCode.OK;
        }

        public INT1448Exception(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.StatusCode = statusCode;
        }

        public INT1448Exception(HttpStatusCode statusCode, string message, Exception inner)
            : base(message, inner)
        {
            this.StatusCode = statusCode;
        }
    }
}
