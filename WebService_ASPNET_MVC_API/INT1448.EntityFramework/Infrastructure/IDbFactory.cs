using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Infrastructure
{
    public interface IDbFactory
    {
        INT1448DbContext Init();
    }
}
