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
    public class BookCategoryService : IBookCategoryService
    {
        private IBookCategoryRepository _bookCategoryRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BookCategoryService(IBookCategoryRepository bookCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._bookCategoryRepository = bookCategoryRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<BookCategoryDTO> Add(BookCategoryDTO bookCategoryDto)
        {
            Func<Task<BookCategoryDTO>> AddAsync = async () => {

                BookCategory bookCategoryAdd = _mapper.Map<BookCategoryDTO, BookCategory>(bookCategoryDto);
                BookCategory bookCategoryAdded = await _bookCategoryRepository.AddAsync(bookCategoryAdd);
                return _mapper.Map<BookCategory, BookCategoryDTO>(bookCategoryAdded);
            };

            return await Task.Run(AddAsync);
        }

        public async Task<BookCategoryDTO> Delete(int id)
        {
            Func<Task<BookCategoryDTO>> DeleteAsync = async () => {

                BookCategory bookCategoryDeleted = await _bookCategoryRepository.DeleteAsync(id);
                return _mapper.Map<BookCategory, BookCategoryDTO>(bookCategoryDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<BookCategoryDTO> Delete(BookCategoryDTO bookCategoryDto)
        {
            Func<Task<BookCategoryDTO>> DeleteAsync = async () => {

                BookCategory bookCategoryDelete = _mapper.Map<BookCategoryDTO, BookCategory>(bookCategoryDto);
                BookCategory bookCategoryDeleted = await _bookCategoryRepository.AddAsync(bookCategoryDelete);
                return _mapper.Map<BookCategory, BookCategoryDTO>(bookCategoryDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<IEnumerable<BookCategoryDTO>> GetAll()
        {
            Func<Task<IEnumerable<BookCategoryDTO>>> GetAllAsync = async () => {

                IEnumerable<BookCategory> bookCategoryFound = await _bookCategoryRepository.GetAllAsync();

                IEnumerable<BookCategoryDTO> bookCategoryDTOs = bookCategoryFound.ForEach<BookCategory, BookCategoryDTO>((item) => {
                    return _mapper.Map<BookCategory, BookCategoryDTO>(item);
                });
                return bookCategoryDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<IEnumerable<BookCategoryDTO>> GetAll(string keyword)
        {
            Func<Task<IEnumerable<BookCategoryDTO>>> GetAllAsync = async () => {
                IEnumerable<BookCategory> bookCategoryFound;

                if (!String.IsNullOrEmpty(keyword))
                {
                    bookCategoryFound = await _bookCategoryRepository.GetMultiAsync(item => (item.Name.Contains(keyword)));
                }
                else
                {
                    bookCategoryFound = await _bookCategoryRepository.GetAllAsync();
                }

                IEnumerable<BookCategoryDTO> bookCategoryDTOs = bookCategoryFound.ForEach<BookCategory, BookCategoryDTO>((item) => {
                    return _mapper.Map<BookCategory, BookCategoryDTO>(item);
                });
                return bookCategoryDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<BookCategoryDTO> GetById(int id)
        {
            Func<Task<BookCategoryDTO>> GetByIdAsync = async () => {
                BookCategory bookCategoryFound = await _bookCategoryRepository.GetSingleByIdAsync(id);
                return _mapper.Map<BookCategory, BookCategoryDTO>(bookCategoryFound);
            };

            return await Task.Run(GetByIdAsync);
        }

        public async Task SaveToDb()
        {
            await _unitOfWork.Commit();
        }

        public async Task Update(BookCategoryDTO bookCategoryDto)
        {
            Func<Task> UpdateAsync = async () => {

                BookCategory bookCategoryUpdate = _mapper.Map<BookCategoryDTO, BookCategory>(bookCategoryDto);
                await _bookCategoryRepository.UpdateAsync(bookCategoryUpdate);
            };

            await Task.Run(UpdateAsync);
        }
    }
}
