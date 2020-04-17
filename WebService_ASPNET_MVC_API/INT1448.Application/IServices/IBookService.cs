using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.Infrastructure.ViewModels;
using INT1448.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.IServices
{
    public interface IBookService : IConvertToViewModel<Book, BookViewModel>
    {
        Task<BookDTO> Add(BookDTO bookDto);

        Task Update(BookDTO bookDto);

        Task<BookDTO> Delete(int id);

        Task<BookDTO> Delete(BookDTO bookDto);

        Task<BookDTO> GetById(int id);

        Task<IEnumerable<BookDTO>> GetAll();

        Task<IEnumerable<BookDTO>> GetAll(string keyword);

        Task<IEnumerable<BookDTO>> GetManyAsync(Expression<Func<Book, bool>> where, string[] includes = null);

        Task SaveToDb();
    }
}
