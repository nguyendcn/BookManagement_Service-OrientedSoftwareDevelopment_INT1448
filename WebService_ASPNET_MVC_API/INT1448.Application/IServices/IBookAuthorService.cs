using INT1448.Application.Infrastructure.DTOs;
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
        Task<BookAuthorDTO> Add(BookAuthorDTO bookAuthorDto);

        Task Update(BookAuthorDTO bookAuthorDto);

        Task<BookAuthorDTO> Delete(int bookId, int authorId);

        Task<BookAuthorDTO> Delete(BookAuthorDTO bookAuthorDto);

        Task<BookAuthorDTO> GetById(int bookId, int authorId);

        Task<IEnumerable<BookAuthorDTO>> GetByBookId(int id);

        Task<IEnumerable<BookAuthorDTO>> GetByAuthorId(int id);

        Task SaveToDb();
    }
}
