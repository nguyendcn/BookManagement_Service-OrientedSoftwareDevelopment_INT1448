using INT1448.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.IServices
{
    public interface IBookService
    {
        Task<Book> Add(Book book);

        Task Update(Book book);

        Task<Book> Delete(int id);

        Task<Book> Delete(Book book);

        Task<Book> GetById(int id);

        Task<IEnumerable<Book>> GetAll();

        Task<IEnumerable<Book>> GetAll(string keyword);

        Task<IEnumerable<Book>> GetManyAsync(Expression<Func<Book, bool>> where, string[] includes = null);

        Task SaveToDb();
    }
}
