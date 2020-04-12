using INT1448.Application.IServices;
using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using INT1448.EntityFramework.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private IBookCategoryRepository _bookCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public BookCategoryService(IBookCategoryRepository bookCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._bookCategoryRepository = bookCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<BookCategory> Add(BookCategory bookCategory)
        {
            return await _bookCategoryRepository.AddAsync(bookCategory);
        }

        public async Task<BookCategory> Delete(int id)
        {
            return await _bookCategoryRepository.DeleteAsync(id);
        }

        public async Task<BookCategory> Delete(BookCategory bookCategory)
        {
            return await _bookCategoryRepository.DeleteAsync(bookCategory);
        }

        public async Task<IEnumerable<BookCategory>> GetAll()
        {
            return await _bookCategoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<BookCategory>> GetAll(string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                return await _bookCategoryRepository.GetMultiAsync(item => (item.Name.Contains(keyword)));
            }
            else
            {
                return await _bookCategoryRepository.GetAllAsync();
            }
        }

        public async Task<BookCategory> GetById(int id)
        {
            return await _bookCategoryRepository.GetSingleByIdAsync(id);
        }

        public async Task SaveToDb()
        {
            await _unitOfWork.Commit();
        }

        public async Task Update(BookCategory bookCategory)
        {
            await _bookCategoryRepository.UpdateAsync(bookCategory);
        }
    }
}
