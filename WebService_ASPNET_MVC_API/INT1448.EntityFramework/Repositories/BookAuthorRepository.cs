using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using INT1448.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Repositories
{
    public interface IBookAuthorRepository: IRepository<BookAuthor>
    {
        Task<BookAuthor> DeleteByBookAuthorIdAsync(int bookId, int authorId);
    }
    public class BookAuthorRepository : RepositoryBase<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
            
        }

        public async Task<BookAuthor> DeleteByBookAuthorIdAsync(int bookId, int authorId)
        {
            IDbSet<BookAuthor> dbSet = this.DbContext.Set<BookAuthor>();

            Func<BookAuthor> Delete = () => {
                var entity = dbSet.Find(bookId, authorId);
                if (entity == null)
                {
                    throw new INT1448Exception(HttpStatusCode.NotFound, $"{DateTime.Now}: Entity was null");
                }

                try
                {
                    return dbSet.Remove(entity);
                }
                catch (Exception ex)
                {
                    throw new INT1448Exception(HttpStatusCode.NotFound, $"{DateTime.Now}: Can not deleted entity.", ex);
                }
            };
            return await Task.Run(Delete);
        }
    }
}
