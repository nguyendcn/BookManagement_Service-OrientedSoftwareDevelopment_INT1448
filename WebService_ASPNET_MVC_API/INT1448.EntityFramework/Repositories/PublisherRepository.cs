using INT1448.Core;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Repositories
{
    public interface IPublisherRepository : IRepository<Publisher>
    {

    }
    public class PublisherRepository : RepositoryBase<Publisher>, IPublisherRepository 
    {
        public PublisherRepository(DbFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
