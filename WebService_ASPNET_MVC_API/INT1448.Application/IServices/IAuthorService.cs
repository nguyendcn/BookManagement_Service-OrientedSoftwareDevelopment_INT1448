using INT1448.Application.Infrastructure.DTOs;
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
        Task<AuthorDTO> Add(AuthorDTO author);

        Task Update(AuthorDTO author);

        Task<AuthorDTO> Delete(int id);

        Task<AuthorDTO> Delete(AuthorDTO author);

        Task<AuthorDTO> GetById(int id);

        Task<IEnumerable<AuthorDTO>> GetAll();

        Task<IEnumerable<AuthorDTO>> GetAll(string keyword);

        Task SaveToDb();
    }
}
