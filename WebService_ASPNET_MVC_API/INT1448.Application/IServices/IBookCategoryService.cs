using INT1448.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.IServices
{
    public interface IBookCategoryService
    {
        Task<BookCategory> Add(BookCategory bookCategory);

        Task Update(BookCategory bookCategory);

        Task<BookCategory> Delete(int id);

        Task<BookCategory> Delete(BookCategory bookCategory);

        Task<BookCategory> GetById(int id);

        Task<IEnumerable<BookCategory>> GetAll();

        Task<IEnumerable<BookCategory>> GetAll(string keyword);

        Task SaveToDb();
    }
}
