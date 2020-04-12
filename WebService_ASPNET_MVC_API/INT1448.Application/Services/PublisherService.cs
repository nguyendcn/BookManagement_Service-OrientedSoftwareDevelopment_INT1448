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

namespace INT1448.Application.Services
{
    public class PublisherService : IPublisherService
    {
        private IPublisherRepository _publisherRepository;
        private IUnitOfWork _unitOfWork;

        public PublisherService(IPublisherRepository publisherRepository, IUnitOfWork unitOfWork)
        {
            this._publisherRepository = publisherRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Publisher> Add(Publisher publisher)
        {
            return await _publisherRepository.AddAsync(publisher);
        }

        public async Task<Publisher> Delete(int id)
        {

             return await _publisherRepository.DeleteAsync(id);
        }

        public async Task<Publisher> Delete(Publisher publisher)
        {
            return await _publisherRepository.DeleteAsync(publisher);
        }

        public async Task<IEnumerable<Publisher>> GetAll()
        {
            return await _publisherRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Publisher>> GetAll(string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                return await _publisherRepository.GetMultiAsync(item => ( item.Name.Contains(keyword)));
            }
            else
            {
                return await _publisherRepository.GetAllAsync();
            }
        }

        public Task<Publisher> GetById(int id)
        {
            return _publisherRepository.GetSingleByIdAsync(id);
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

        public async Task Update(Publisher publisher)
        {
            await _publisherRepository.UpdateAsync(publisher);
        }
    }
}
