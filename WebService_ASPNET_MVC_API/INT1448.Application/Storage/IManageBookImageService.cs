using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace INT1448.Application.Storage
{
    public interface IManageBookImageService
    {
        Task SaveImage(IList<HttpContent> files, int bookId);

        Task DeleteByBookId(int bookId);

        Task DeleteMulti(IEnumerable<string> fileNames);
    }
}