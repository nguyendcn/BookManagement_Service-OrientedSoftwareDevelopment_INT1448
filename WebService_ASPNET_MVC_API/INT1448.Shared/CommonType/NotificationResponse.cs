using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Shared.CommonType
{
    public class NotificationResponse : ParameterError
    {
        public NotificationResponse(string success, string reason) : base(success, reason)
        {
        }
    }
}
