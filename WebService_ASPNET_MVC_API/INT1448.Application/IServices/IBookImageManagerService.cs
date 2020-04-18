using INT1448.Application.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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

        Task<IEnumerable<BookImageDTO>> GetAllByBookId(int bookId);

        Task DeleteAllByBookId(int bookId);

        Task SaveToDb();
    }
}
