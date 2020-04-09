using INT1448.EntityFramework.EntityFramework.Infrastructure;
using INT1448.EntityFramework.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INT1448.Application.IServices;
using INT1448.Core;
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

        public Publisher Add(Publisher publisher)
        {
            return _publisherRepository.Add(publisher);
        }

        public Publisher Delete(int id)
        {
            return _publisherRepository.Delete(id);
        }

        public Publisher Delete(Publisher publisher)
        {
            return _publisherRepository.Delete(publisher);
        }

        public IEnumerable<Publisher> GetAll()
        {
            return _publisherRepository.GetAll();
        }

        public IEnumerable<Publisher> GetAll(string keyword)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                return _publisherRepository.GetMulti(item => ( item.Name.Contains(keyword)));
            }
            else
            {
                return _publisherRepository.GetAll();
            }
        }

        public Publisher GetById(int id)
        {
            return _publisherRepository.GetSingleById(id);
        }

        public void SaveToDb()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Publisher> Search(
            string keyword, int page, int pageSize, ESortMode sort, out int totalRow)
        {
            var searchResult = GetAll(keyword);

            totalRow = searchResult.Count();
            
            switch (sort)
            {
                case ESortMode.DESC:
                    return searchResult.OrderByDescending(item => item.ID).Skip(page * pageSize).Take(pageSize);
                    break;
                case ESortMode.ASC:
                    return searchResult.OrderByDescending(item => item.ID).Skip(page * pageSize).Take(pageSize);
                    break;
                default:
                    return searchResult.OrderByDescending(item => item.ID).Skip(page * pageSize).Take(pageSize);
                    break;
            }
        }

        public void Update(Publisher publisher)
        {
            _publisherRepository.Update(publisher);
        }
    }
}
