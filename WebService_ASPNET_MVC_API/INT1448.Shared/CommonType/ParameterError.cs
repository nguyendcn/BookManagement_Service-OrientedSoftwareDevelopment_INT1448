using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Shared.CommonType
{
    public class ParameterError
    {
        public string Success { get; set; }
        public string Reason { get; set; }

        public ParameterError(string success, string reason)
        {
            this.Success = success;
            this.Reason = reason;
        }
    }
}
