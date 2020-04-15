using INT1448.Application.Infrastructure.DTOs;
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
        Task<BookCategoryDTO> Add(BookCategoryDTO bookCategoryDto);

        Task Update(BookCategoryDTO bookCategoryDto);

        Task<BookCategoryDTO> Delete(int id);

        Task<BookCategoryDTO> Delete(BookCategoryDTO bookCategoryDto);

        Task<BookCategoryDTO> GetById(int id);

        Task<IEnumerable<BookCategoryDTO>> GetAll();

        Task<IEnumerable<BookCategoryDTO>> GetAll(string keyword);

        Task SaveToDb();
    }
}
