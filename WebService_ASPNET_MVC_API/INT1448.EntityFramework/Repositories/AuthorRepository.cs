using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {

    }
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(DbFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
