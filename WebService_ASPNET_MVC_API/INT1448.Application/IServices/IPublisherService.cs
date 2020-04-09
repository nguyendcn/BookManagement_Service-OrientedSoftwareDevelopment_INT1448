using INT1448.Core;
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
        Publisher Add(Publisher publisher);

        void Update(Publisher publisher);

        Publisher Delete(int id);

        Publisher Delete(Publisher publisher);

        Publisher GetById(int id);

        IEnumerable<Publisher> GetAll();

        IEnumerable<Publisher> GetAll(string keyword);

        IEnumerable<Publisher> Search(string keyword, int page, int pageSize, ESortMode sort, out int totalRow);

        void SaveToDb();

    }
}
