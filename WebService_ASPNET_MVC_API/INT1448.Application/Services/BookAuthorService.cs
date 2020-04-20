using AutoMapper;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Application.Infrastructure.Exeptions;
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
    public class BookAuthorService : IBookAuthorService
    {
        private IBookAuthorRepository _bookAuthorRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BookAuthorService(IBookAuthorRepository bookAuthorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._bookAuthorRepository = bookAuthorRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<BookAuthorDTO> Add(BookAuthorDTO bookAuthorDto)
        {
            Func<Task<BookAuthorDTO>> AddAsync = async () => {

                BookAuthor bookAuthorAdd = _mapper.Map<BookAuthorDTO, BookAuthor>(bookAuthorDto);
                BookAuthor authorAdded = await _bookAuthorRepository.AddAsync(bookAuthorAdd);
                return _mapper.Map<BookAuthor, BookAuthorDTO>(authorAdded);
            };

            return await Task.Run(AddAsync);
        }

        public async Task<BookAuthorDTO> Delete(int bookId, int authorId)
        {
            Func<Task<BookAuthorDTO>> DeleteAsync = async () => {

                BookAuthor bookAuthorDeleted = await _bookAuthorRepository.DeleteByBookAuthorIdAsync(bookId, authorId);
                return _mapper.Map<BookAuthor, BookAuthorDTO>(bookAuthorDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<BookAuthorDTO> Delete(BookAuthorDTO bookAuthorDto)
        {
            Func<Task<BookAuthorDTO>> DeleteAsync = async () => {

                BookAuthor bookAuthorDelete = _mapper.Map<BookAuthorDTO, BookAuthor>(bookAuthorDto);
                BookAuthor authorDeleted = await _bookAuthorRepository.AddAsync(bookAuthorDelete);
                return _mapper.Map<BookAuthor, BookAuthorDTO>(authorDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<IEnumerable<BookAuthorDTO>> GetByAuthorId(int id)
        {
            Func<Task<IEnumerable<BookAuthorDTO>>> GetByIdAsync = async () => {
                IEnumerable<BookAuthor> bookAuthorsFound = await _bookAuthorRepository.GetMultiAsync(x => x.AuthorID == id);

                IEnumerable<BookAuthorDTO> bookAuthorDTOs = bookAuthorsFound.ForEach<BookAuthor, BookAuthorDTO>((item) => {
                    return _mapper.Map<BookAuthor, BookAuthorDTO>(item);
                });
                return bookAuthorDTOs;
            };

            return await Task.Run(GetByIdAsync);
        }

        public async Task<IEnumerable<BookAuthorDTO>> GetByBookId(int id)
        {
            Func<Task<IEnumerable<BookAuthorDTO>>> GetByIdAsync = async () => {
                IEnumerable<BookAuthor> bookAuthorsFound = await _bookAuthorRepository.GetMultiAsync(x => x.BookID == id);

                IEnumerable<BookAuthorDTO> bookAuthorDTOs = bookAuthorsFound.ForEach<BookAuthor, BookAuthorDTO>((item) => {
                    return _mapper.Map<BookAuthor, BookAuthorDTO>(item);
                });
                return bookAuthorDTOs;
            };

            return await Task.Run(GetByIdAsync);
        }

        public async Task<BookAuthorDTO> GetById(int bookId, int authorId)
        {
            Func<Task<BookAuthorDTO>> GetByIdAsync = async () => {

                IEnumerable<BookAuthor> bookAuthors = await _bookAuthorRepository.GetMultiAsync(x => (x.BookID == bookId && x.AuthorID == authorId));

                IEnumerable<BookAuthorDTO> bookAuthorDTOs = bookAuthors.ForEach<BookAuthor, BookAuthorDTO>((item) => {
                    return _mapper.Map<BookAuthor, BookAuthorDTO>(item);
                });
                return bookAuthorDTOs.FirstOrDefault();
            };

            return await Task.Run(GetByIdAsync);
        }

        public async Task SaveToDb()
        {
            await _unitOfWork.Commit();
        }

        public async Task Update(BookAuthorDTO bookAuthorDto)
        {
            Func<Task> UpdateAsync = async () => {

                BookAuthor bookAuthorUpdate = _mapper.Map<BookAuthorDTO, BookAuthor>(bookAuthorDto);
                await _bookAuthorRepository.UpdateAsync(bookAuthorUpdate);
            };

            await Task.Run(UpdateAsync);
        }

        public async Task Update(IEnumerable<int> authorIDs, int bookId)
        {
            IEnumerable<BookAuthorDTO> bookAuthors = await GetByBookId(bookId);

            IEnumerable<int> dbAuthors = (bookAuthors).Select(x => x.AuthorID);

            IEnumerable<int> join = from src in authorIDs
                                       join db in dbAuthors
                                       on src equals db
                                       select src;
            int joinCount = join.Count();
            int srcCount = authorIDs.Count();
            int dbCount = dbAuthors.Count();

            if (joinCount == srcCount && joinCount == dbCount) //not changed
            {
                return;
            }
            else if (joinCount < srcCount && joinCount == dbCount) //insert
            {
                IEnumerable<int> diffirents = authorIDs.Except(dbAuthors);
                foreach (int author in diffirents)
                {
                    await Add(new BookAuthorDTO() { BookID = bookId, AuthorID = author });
                }
            }
            else if (joinCount == srcCount && joinCount < dbCount) //delete
            {
                IEnumerable<int> diffirents = dbAuthors.Except(authorIDs);
                foreach (int author in diffirents)
                {
                    BookAuthorDTO bookAuthor = bookAuthors.Where(x => x.AuthorID == author).Single();
                    await Delete(bookAuthor.BookID, bookAuthor.AuthorID);
                }
                await SaveToDb();
            }
            else if (joinCount < srcCount && joinCount < dbCount) // insert and delete
            {
                IEnumerable<int> inserted = authorIDs.Except(dbAuthors);
                IEnumerable<int> deleted = dbAuthors.Except(authorIDs);

                foreach (int author in inserted)
                {
                    await Add(new BookAuthorDTO() { BookID = bookId, AuthorID = author });
                }

                foreach (int author in deleted)
                {
                    BookAuthorDTO bookAuthor = bookAuthors.Where(x => x.AuthorID == author).Single();
                    await Delete(bookAuthor.BookID, bookAuthor.AuthorID);
                }
                await SaveToDb();
            }
            else
            {
                throw new INT1448Exception("Can not update book author.");
            }
        }
    }
}
