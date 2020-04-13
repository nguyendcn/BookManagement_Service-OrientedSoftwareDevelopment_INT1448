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
    public class BookAuthorService : IBookAuthorService
    {
        private IBookAuthorRepository _bookAuthorRepository;
        private IUnitOfWork _unitOfWork;

        public BookAuthorService(IBookAuthorRepository bookAuthorRepository, IUnitOfWork unitOfWork)
        {
            this._bookAuthorRepository = bookAuthorRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<BookAuthor> Add(BookAuthor bookAuthor)
        {
            return await _bookAuthorRepository.AddAsync(bookAuthor);
        }

        public async Task<BookAuthor> Delete(int bookId, int authorId)
        {
            return await _bookAuthorRepository.DeleteByBookAuthorIdAsync(bookId, authorId);
        }

        public async Task<BookAuthor> Delete(BookAuthor bookAuthor)
        {
            return await _bookAuthorRepository.DeleteAsync(bookAuthor);
        }

        public async Task<IEnumerable<BookAuthor>> GetByAuthorId(int id)
        {
            return await _bookAuthorRepository.GetMultiAsync(x=>x.AuthorID == id);
        }

        public async Task<IEnumerable<BookAuthor>> GetByBookId(int id)
        {
            return await _bookAuthorRepository.GetMultiAsync(x => x.BookID == id);
        }

        public async Task<BookAuthor> GetById(int bookId, int authorId)
        {
            IEnumerable<BookAuthor> bookAuthors = await _bookAuthorRepository.GetMultiAsync(x => (x.BookID == bookId && x.AuthorID == authorId));

            return bookAuthors.FirstOrDefault();
        }

        public async Task SaveToDb()
        {
            await _unitOfWork.Commit();
        }

        public async Task Update(BookAuthor bookAuthor)
        {
            await _bookAuthorRepository.UpdateAsync(bookAuthor);
        }
    }
}
