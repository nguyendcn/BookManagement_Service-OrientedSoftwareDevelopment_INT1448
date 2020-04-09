using INT1448.Core.Models;
using INT1448.Shared.CommonType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.IServices
{
    public interface IPublisherService
    {
        Task<Publisher> Add(Publisher publisher);

        Task Update(Publisher publisher);

        Task<Publisher> Delete(int id);

        Task<Publisher> Delete(Publisher publisher);

        Task<Publisher> GetById(int id);

        Task<IEnumerable<Publisher>> GetAll();

        Task<IEnumerable<Publisher>> GetAll(string keyword);

        /// <summary>
        /// Search records by keyword.
        /// </summary>
        /// <param name="keyword">Keyword to searching.</param>
        /// <param name="page">Page want to take.</param>
        /// <param name="pageSize">Quantity records per page.</param>
        /// <param name="sort">Mode to sort.</param>
        /// <returns>
        ///  Just have twos items
        ///  First index: All records match with keyword and pagging
        ///  Last index: Total record without pagging 
        ///  </returns>
        Task<object[]> Search(string keyword, int page, int pageSize, ESortMode sort);

        Task SaveToDb();

    }
}
