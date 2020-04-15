using INT1448.EntityFramework.EntityFramework.Infrastructure;
using INT1448.EntityFramework.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INT1448.Application.IServices;
using INT1448.Core.Models;
using INT1448.Shared.CommonType;
using AutoMapper;
using INT1448.Application.Infrastructure.DTOs;
using INT1448.Shared.Extensions;

namespace INT1448.Application.Services
{
    public class PublisherService : IPublisherService
    {
        private IPublisherRepository _publisherRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PublisherService(IPublisherRepository publisherRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._publisherRepository = publisherRepository;
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task<PublisherDTO> Add(PublisherDTO publisherDto)
        {
            Func<Task<PublisherDTO>> AddAsync = async () => {

                Publisher authorAdd = _mapper.Map<PublisherDTO, Publisher>(publisherDto);
                Publisher authorAdded = await _publisherRepository.AddAsync(authorAdd);
                return _mapper.Map<Publisher, PublisherDTO>(authorAdded);
            };

            return await Task.Run(AddAsync);
        }

        public async Task<PublisherDTO> Delete(int id)
        {

            Func<Task<PublisherDTO>> DeleteAsync = async () => {

                Publisher authorDeleted = await _publisherRepository.DeleteAsync(id);
                return _mapper.Map<Publisher, PublisherDTO>(authorDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<PublisherDTO> Delete(PublisherDTO publisherDto)
        {
            Func<Task<PublisherDTO>> DeleteAsync = async () => {

                Publisher authorDelete = _mapper.Map<PublisherDTO, Publisher>(publisherDto);
                Publisher authorDeleted = await _publisherRepository.AddAsync(authorDelete);
                return _mapper.Map<Publisher, PublisherDTO>(authorDeleted);
            };

            return await Task.Run(DeleteAsync);
        }

        public async Task<IEnumerable<PublisherDTO>> GetAll()
        {
            Func<Task<IEnumerable<PublisherDTO>>> GetAllAsync = async () => {

                IEnumerable<Publisher> authorFound = await _publisherRepository.GetAllAsync();

                IEnumerable<PublisherDTO> authorDTOs = authorFound.ForEach<Publisher, PublisherDTO>((item) => {
                    return _mapper.Map<Publisher, PublisherDTO>(item);
                });
                return authorDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<IEnumerable<PublisherDTO>> GetAll(string keyword)
        {
            Func<Task<IEnumerable<PublisherDTO>>> GetAllAsync = async () => {
                IEnumerable<Publisher> authorFound;

                if (!String.IsNullOrEmpty(keyword))
                {
                    authorFound = await _publisherRepository.GetMultiAsync(item => (item.Name.Contains(keyword)));
                }
                else
                {
                    authorFound = await _publisherRepository.GetAllAsync();
                }

                IEnumerable<PublisherDTO> authorDTOs = authorFound.ForEach<Publisher, PublisherDTO>((item) => {
                    return _mapper.Map<Publisher, PublisherDTO>(item);
                });
                return authorDTOs;
            };

            return await Task.Run(GetAllAsync);
        }

        public async Task<PublisherDTO> GetById(int id)
        {
            Func<Task<PublisherDTO>> GetByIdAsync = async () => {
                Publisher authorFound = await _publisherRepository.GetSingleByIdAsync(id);
                return _mapper.Map<Publisher, PublisherDTO>(authorFound);
            };

            return await Task.Run(GetByIdAsync);
        }

        public Task SaveToDb()
        {
            return _unitOfWork.Commit();
        }

        public async Task DelAll()
        {
            await _publisherRepository.DeleteMultiAsync(x=>x.ID == 1);
        }

        public async Task<object[]> Search(
            string keyword, int page, int pageSize, ESortMode sort)
        {
            var searchResult = await GetAll(keyword);

            int totalRow =  searchResult.Count();

            Func<object[]> SortAsync = () => {
                switch (sort)
                {
                    case ESortMode.DESC:
                        return new object[] { searchResult.OrderByDescending(item => item.ID).Skip(page * pageSize).Take(pageSize), totalRow };
                        break;
                    case ESortMode.ASC:
                        return new object[] { searchResult.OrderByDescending(item => item.ID).Skip(page * pageSize).Take(pageSize), totalRow };
                        break;
                    default:
                        return new object[] { searchResult.OrderByDescending(item => item.ID).Skip(page * pageSize).Take(pageSize), totalRow };
                        break;
                }
            };

            return await Task.Run(() => SortAsync());
        }

        public async Task Update(PublisherDTO publisherDto)
        {
            Func<Task> UpdateAsync = async () => {

                Publisher authorUpdate = _mapper.Map<PublisherDTO, Publisher>(publisherDto);
                await _publisherRepository.UpdateAsync(authorUpdate);
            };

            await Task.Run(UpdateAsync);
        }
    }
}
