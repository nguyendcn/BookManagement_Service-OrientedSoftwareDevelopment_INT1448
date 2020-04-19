using INT1448.Application.Infrastructure.DTOs;
using INT1448.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.IServices
{
    public interface IBookImageManagerService
    {
        Task<BookImageDTO> Add(BookImageDTO bookImageDto);

        Task Update(BookImageDTO bookImageDto);

        Task<BookImageDTO> Delete(int id);

        Task<BookImageDTO> Delete(BookImageDTO bookImageDto);

        Task<BookImageDTO> GetById(int id);

        Task<IEnumerable<BookImageDTO>> GetAll();

        Task<BookImageDTO> GetByCondition(Expression<Func<BookImage, bool>> expression);

        Task<IEnumerable<BookImageDTO>> GetAllByBookId(int bookId);

        Task DeleteAllByBookId(int bookId);

        Task SaveToDb();
    }
}
