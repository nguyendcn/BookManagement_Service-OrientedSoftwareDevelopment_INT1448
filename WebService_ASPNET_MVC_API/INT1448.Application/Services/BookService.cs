using INT1448.Application.IServices;
using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using INT1448.EntityFramework.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Services
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        private IUnitOfWork _unitOfWork;
        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            this._bookRepository = bookRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Book> Add(Book book)
        {
            return await _bookRepository.AddAsync(book);
        }

        public async Task<Book> Delete(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }

        public async Task<Book> Delete(Book book)
        {
            return await _bookRepository.DeleteAsync(book);
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Book>> GetAll(string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                return await _bookRepository.GetMultiAsync(item => (item.Name.Contains(keyword)));
            }
            else
            {
                return await _bookRepository.GetAllAsync();
            }
        }

        public async Task<Book> GetById(int id)
        {
            return await _bookRepository.GetSingleByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetManyAsync(Expression<Func<Book, bool>> where, string[] includes=null)
        {
            return await _bookRepository.GetMultiAsync(where, includes);
        }

        public async Task SaveToDb()
        {
            await _unitOfWork.Commit();
        }

        public async Task Update(Book book)
        {
            await _bookRepository.UpdateAsync(book);
        }
    }
}
