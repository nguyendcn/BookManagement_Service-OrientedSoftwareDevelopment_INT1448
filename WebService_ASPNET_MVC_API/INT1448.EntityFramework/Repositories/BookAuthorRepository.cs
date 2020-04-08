using INT1448.Core;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Repositories
{
    public interface IBookAuthorRepository: IRepository<BookAuthor>
    {

    }
    public class BookAuthorRepository : RepositoryBase<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(DbFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
