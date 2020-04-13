using INT1448.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.IServices
{
    public interface IAuthorService
    {
        Task<Author> Add(Author author);

        Task Update(Author author);

        Task<Author> Delete(int id);

        Task<Author> Delete(Author author);

        Task<Author> GetById(int id);

        Task<IEnumerable<Author>> GetAll();

        Task<IEnumerable<Author>> GetAll(string keyword);

        Task SaveToDb();
    }
}
