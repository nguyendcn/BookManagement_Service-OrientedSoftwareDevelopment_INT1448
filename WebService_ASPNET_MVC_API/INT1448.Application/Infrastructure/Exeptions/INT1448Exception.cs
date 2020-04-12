using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Infrastructure.Exeptions
{
    public class INT1448Exception : Exception
    {
        public INT1448Exception() : base()
        {

        }

        public INT1448Exception(string message)
            : base(message)
        {
        }

        public INT1448Exception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
