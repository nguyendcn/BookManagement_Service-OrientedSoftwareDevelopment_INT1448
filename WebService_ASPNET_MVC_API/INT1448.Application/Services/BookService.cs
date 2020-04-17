using AutoMapper;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.Infrastructure.ViewModels;
using INT1448.Application.IServices;
using INT1448.Core.Models;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using INT1448.EntityFramework.EntityFramework.Repositories;
using INT1448.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace INT1448.Application.Services
{
    public class BookService : ConvertToViewModel<Book, BookViewModel>, IBookService
    {
        private IBookRepository _bookRepository;
        private IBookImageManagerService _bookImageManagerService;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BookService(IBookRepository bookRepository,
            IBookImageManagerService bookImageManagerService,
            IUnitOfWork unitOfWork, IMapper mapper, IDbFactory dbFactory)
            : base(dbFactory, mapper)
        {
            this._bookRepository = bookRepository;
            this._bookImageManagerService = bookImageManagerService;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<BookDTO> Add(BookDTO bookDto)
        {
            Func<Task<BookDTO>> AddAsync = async () => {

                Book bookAdd = _mapper.Map<BookDTO, Book>(bookDto);
                Book bookAdded = await _bookRepository.AddAsync(bookAdd);
                return _mapper.Map<Book, BookDTO>(bookAdded);
            };

            return await Task.Run(AddAsync);
        }

        public async Task<BookDTO> Delete(int id)
        {
            Func<Task<BookDTO>> DeleteAsync = async () => {

                Book bookDeleted = await _bookRepository.DeleteAsync(id);
                return _mapper.Map<Book, BookDTO>(bookDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<BookDTO> Delete(BookDTO bookDto)
        {
            Func<Task<BookDTO>> DeleteAsync = async () => {

                Book bookDelete = _mapper.Map<BookDTO, Book>(bookDto);
                Book bookDeleted = await _bookRepository.AddAsync(bookDelete);
                return _mapper.Map<Book, BookDTO>(bookDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<IEnumerable<BookDTO>> GetAll()
        {
            Func<Task<IEnumerable<BookDTO>>> GetAllAsync = async () => {

                IEnumerable<Book> bookFound = await _bookRepository.GetAllAsync();

                IEnumerable<BookDTO> bookDTOs = bookFound.ForEach<Book, BookDTO>((item) => {
                    return _mapper.Map<Book, BookDTO>(item);
                });
                return bookDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<IEnumerable<BookDTO>> GetAll(string keyword)
        {
            Func<Task<IEnumerable<BookDTO>>> GetAllAsync = async () => {
                IEnumerable<Book> bookFound;

                if (!String.IsNullOrEmpty(keyword))
                {
                    bookFound = await _bookRepository.GetMultiAsync(item => (item.Name.Contains(keyword)));
                }
                else
                {
                    bookFound = await _bookRepository.GetAllAsync();
                }

                IEnumerable<BookDTO> bookDTOs = bookFound.ForEach<Book, BookDTO>((item) => {
                    return _mapper.Map<Book, BookDTO>(item);
                });
                return bookDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<BookDTO> GetById(int id)
        {
            Func<Task<BookDTO>> GetByIdAsync = async () => {
                Book bookFound = await _bookRepository.GetSingleByIdAsync(id);
                BookDTO bookDTO = _mapper.Map<Book, BookDTO>(bookFound);
                //bookDTO.ImagesUrl = await _bookImageManagerService.GetAllByBookId(id);

                return bookDTO;
            };

            return await Task.Run(GetByIdAsync);
        }

        public async Task<IEnumerable<BookDTO>> GetManyAsync(Expression<Func<Book, bool>> where, string[] includes=null)
        {
            Func<Task<IEnumerable<BookDTO>>> GetAllAsync = async () => {

                IEnumerable<Book> bookFound = await _bookRepository.GetMultiAsync(where, includes);

                IEnumerable<BookDTO> bookDTOs = bookFound.ForEach<Book, BookDTO>((item) => {
                    return _mapper.Map<Book, BookDTO>(item);
                });
                return bookDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task SaveToDb()
        {
            await _unitOfWork.Commit();
        }

        public async Task Update(BookDTO bookDto)
        {
            Func<Task> UpdateAsync = async () => {

                Book bookUpdate = _mapper.Map<BookDTO, Book>(bookDto);
                await _bookRepository.UpdateAsync(bookUpdate);
            };

            await Task.Run(UpdateAsync);
        }
    }
}
