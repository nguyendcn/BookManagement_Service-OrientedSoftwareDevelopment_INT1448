using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {

    }

    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {

        public BookRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
