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
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;
        private IUnitOfWork _unitOfWork;
        public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            this._authorRepository = authorRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<Author> Add(Author author)
        {
            return await _authorRepository.AddAsync(author);
        }

        public async Task<Author> Delete(int id)
        {
            return await _authorRepository.DeleteAsync(id);
        }

        public async Task<Author> Delete(Author author)
        {
            return await _authorRepository.DeleteAsync(author);
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Author>> GetAll(string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                return await _authorRepository.GetMultiAsync(item => (item.FullName.Contains(keyword)));
            }
            else
            {
                return await _authorRepository.GetAllAsync();
            }
        }

        public async Task<Author> GetById(int id)
        {
            return await _authorRepository.GetSingleByIdAsync(id);
        }

        public async Task SaveToDb()
        {
            await _unitOfWork.Commit();
        }

        public async Task Update(Author author)
        {
            await _authorRepository.UpdateAsync(author);
        }
    }
}
