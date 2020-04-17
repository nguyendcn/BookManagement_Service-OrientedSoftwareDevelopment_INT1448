using INT1448.EntityFramework.EntityFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.Application.IServices
{
    public interface IConvertToViewModel<TSource, TDest> where TSource : class where TDest : class
    {
        Task<TDest> GetByIdToView(int id);

        Task<IEnumerable<TDest>> GetAllToView();
    }
}
