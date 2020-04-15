using AutoMapper;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.IServices;
using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using INT1448.EntityFramework.EntityFramework.Repositories;
using INT1448.Shared.Extensions;
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
        private IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._authorRepository = authorRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<AuthorDTO> Add(AuthorDTO authorDto)
        {
            Func<Task<AuthorDTO>> AddAsync = async () => {

                Author authorAdd = _mapper.Map<AuthorDTO, Author>(authorDto);
                Author authorAdded = await _authorRepository.AddAsync(authorAdd);
                return _mapper.Map<Author, AuthorDTO>(authorAdded);
            };

            return await Task.Run(AddAsync);
        }

        public async Task<AuthorDTO> Delete(int id)
        {
            Func<Task<AuthorDTO>> DeleteAsync = async () => {

                Author authorDeleted = await _authorRepository.DeleteAsync(id);
                return _mapper.Map<Author, AuthorDTO>(authorDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<AuthorDTO> Delete(AuthorDTO authorDto)
        {
            Func<Task<AuthorDTO>> DeleteAsync = async () => {

                Author authorDelete = _mapper.Map<AuthorDTO, Author>(authorDto);
                Author authorDeleted = await _authorRepository.AddAsync(authorDelete);
                return _mapper.Map<Author, AuthorDTO>(authorDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAll()
        {
            Func<Task<IEnumerable<AuthorDTO>>> GetAllAsync = async () => {

                IEnumerable<Author> authorFound = await _authorRepository.GetAllAsync();

                IEnumerable<AuthorDTO> authorDTOs = authorFound.ForEach<Author, AuthorDTO>((item) => {
                    return _mapper.Map<Author, AuthorDTO>(item);
                });
                 return authorDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<IEnumerable<AuthorDTO>> GetAll(string keyword)
        {
            Func<Task<IEnumerable<AuthorDTO>>> GetAllAsync = async () => {
                IEnumerable<Author> authorFound;

                if (!String.IsNullOrEmpty(keyword))
                {
                    authorFound = await _authorRepository.GetMultiAsync(item => (item.FullName.Contains(keyword)));
                }
                else
                {
                    authorFound = await _authorRepository.GetAllAsync();
                }

                IEnumerable<AuthorDTO> authorDTOs = authorFound.ForEach<Author, AuthorDTO>((item) => {
                    return _mapper.Map<Author, AuthorDTO>(item);
                });
                return authorDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<AuthorDTO> GetById(int id)
        {
            Func<Task<AuthorDTO>> GetByIdAsync = async () => {
                Author authorFound = await _authorRepository.GetSingleByIdAsync(id);
                return _mapper.Map<Author, AuthorDTO>(authorFound);
            };

            return await Task.Run(GetByIdAsync);
        }

        public async Task SaveToDb()
        {
            await _unitOfWork.Commit();
        }

        public async Task Update(AuthorDTO authorDto)
        {
            Func<Task> UpdateAsync = async () => {

                Author authorUpdate = _mapper.Map<AuthorDTO, Author>(authorDto);
                await _authorRepository.UpdateAsync(authorUpdate);
            };

            await Task.Run(UpdateAsync);
        }
    }
}
