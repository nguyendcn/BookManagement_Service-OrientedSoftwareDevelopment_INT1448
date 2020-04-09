using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Repositories
{
    public interface IBookCategoryRepository : IRepository<BookCategory>
    {
        IEnumerable<BookCategory> GetByAlias(string alias);
    }

    public class BookCategoryRepository : RepositoryBase<BookCategory>, IBookCategoryRepository
    {
        public BookCategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {

        }

        public IEnumerable<BookCategory> GetByAlias(string alias)
        {
            return this.DbContext.BookCategories.Where(x => (x.Alias.Equals(alias)));
        }
    }
}
