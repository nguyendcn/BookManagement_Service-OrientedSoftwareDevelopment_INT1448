using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.Repositories
{
    public interface IBookImageManagerRepository : IRepository<BookImage>
    {

    }

    public class BookImageManagerRepository : RepositoryBase<BookImage>, IBookImageManagerRepository
    {
        public BookImageManagerRepository(IDbFactory dbFactory) : 
            base(dbFactory)
        {
        }
    }
}
