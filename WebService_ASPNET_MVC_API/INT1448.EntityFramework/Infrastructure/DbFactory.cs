using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private INT1448DbContext dbContext;

        public INT1448DbContext Init()
        {
            return dbContext ?? (dbContext = new INT1448DbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
