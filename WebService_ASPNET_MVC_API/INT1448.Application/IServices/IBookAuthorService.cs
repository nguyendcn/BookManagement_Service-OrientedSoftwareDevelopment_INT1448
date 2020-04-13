using INT1448.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.IServices
{
    public interface IBookAuthorService
    {
        Task<BookAuthor> Add(BookAuthor bookAuthor);

        Task Update(BookAuthor bookAuthor);

        Task<BookAuthor> Delete(int bookId, int authorId);

        Task<BookAuthor> Delete(BookAuthor bookAuthor);

        Task<BookAuthor> GetById(int bookId, int authorId);

        Task<IEnumerable<BookAuthor>> GetByBookId(int id);

        Task<IEnumerable<BookAuthor>> GetByAuthorId(int id);

        Task SaveToDb();
    }
}
