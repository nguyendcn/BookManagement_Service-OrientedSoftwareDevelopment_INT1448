using AutoMapper;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.IServices;
using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using INT1448.EntityFramework.Repositories;
using INT1448.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.Services
{
    public class BookImageManagerService : IBookImageManagerService
    {
        private IBookImageManagerRepository _bookImageManagerRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BookImageManagerService(IBookImageManagerRepository bookImageManagerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._bookImageManagerRepository = bookImageManagerRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<BookImageDTO> Add(BookImageDTO bookImageDto)
        {
            Func<Task<BookImageDTO>> AddAsync = async () => {

                BookImage authorAdd = _mapper.Map<BookImageDTO, BookImage>(bookImageDto);
                BookImage authorAdded = await _bookImageManagerRepository.AddAsync(authorAdd);
                return _mapper.Map<BookImage, BookImageDTO>(authorAdded);
            };

            return await Task.Run(AddAsync);
        }

        public async Task<BookImageDTO> Delete(int id)
        {
            Func<Task<BookImageDTO>> DeleteAsync = async () => {

                BookImage authorDeleted = await _bookImageManagerRepository.DeleteAsync(id);
                return _mapper.Map<BookImage, BookImageDTO>(authorDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<BookImageDTO> Delete(BookImageDTO bookImageDto)
        {
            Func<Task<BookImageDTO>> DeleteAsync = async () => {

                BookImage authorDelete = _mapper.Map<BookImageDTO, BookImage>(bookImageDto);
                BookImage authorDeleted = await _bookImageManagerRepository.DeleteAsync(authorDelete);
                return _mapper.Map<BookImage, BookImageDTO>(authorDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task DeleteAllByBookId(int bookId)
        {
            Func<Task> DeleteAsync = async () => {

                await _bookImageManagerRepository.DeleteMultiAsync(b => b.BookId == bookId);
            };

            await Task.Run(DeleteAsync);
        }

        public async Task<IEnumerable<BookImageDTO>> GetAll()
        {
            Func<Task<IEnumerable<BookImageDTO>>> GetAllAsync = async () => {

                IEnumerable<BookImage> bookImages = await _bookImageManagerRepository.GetAllAsync();

                IList<BookImageDTO> bookDTOs = new List<BookImageDTO>();

                foreach(BookImage bookImage in bookImages)
                {
                    bookDTOs.Add(_mapper.Map<BookImageDTO>(bookImage));
                }

                return bookDTOs ;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<IEnumerable<BookImageDTO>> GetAllByBookId(int bookId)
        {
            Func<Task<IEnumerable<BookImageDTO>>> GetAllAsync = async () => {

                IEnumerable<BookImage> bookImageFound = await _bookImageManagerRepository.GetMultiAsync(x=> x.BookId == bookId);

                IList<BookImageDTO> bookImageDTOs = new List<BookImageDTO>();
                foreach(BookImage bi in bookImageFound)
                {
                    bookImageDTOs.Add(_mapper.Map<BookImage, BookImageDTO>(bi));
                }

                return bookImageDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<BookImageDTO> GetByCondition(Expression<Func<BookImage, bool>> expression)
        {
            BookImage bookImage = await _bookImageManagerRepository.GetSingleByConditionAsync(expression);
            return _mapper.Map<BookImageDTO>(bookImage);
        }

        public async Task<BookImageDTO> GetById(int id)
        {
            Func<Task<BookImageDTO>> GetByIdAsync = async () => {
                BookImage authorFound = await _bookImageManagerRepository.GetSingleByIdAsync(id);
                return _mapper.Map<BookImage, BookImageDTO>(authorFound);
            };

            return await Task.Run(GetByIdAsync);
        }

        public async Task SaveToDb()
        {
            await _unitOfWork.Commit();
        }

        public async Task Update(BookImageDTO bookImageDto)
        {
            Func<Task> UpdateAsync = async () => {

                BookImage authorUpdate = _mapper.Map<BookImageDTO, BookImage>(bookImageDto);
                await _bookImageManagerRepository.UpdateAsync(authorUpdate);
            };

            await Task.Run(UpdateAsync);
        }
    }
}
