using INT1448.Application.Infrastructure.DTOs;
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
        Task<PublisherDTO> Add(PublisherDTO publisherDto);

        Task Update(PublisherDTO publisherDto);

        Task<PublisherDTO> Delete(int id);

        Task<PublisherDTO> Delete(PublisherDTO publisherDto);

        Task<PublisherDTO> GetById(int id);

        Task<IEnumerable<PublisherDTO>> GetAll();

        Task<IEnumerable<PublisherDTO>> GetAll(string keyword);

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
