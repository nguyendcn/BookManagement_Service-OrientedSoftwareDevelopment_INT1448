using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        Task<T> AddAsync(T entity);

        // Marks an entity as modified
        Task UpdateAsync(T entity);

        // Marks an entity to be removed
        Task<T> DeleteAsync(T entity);

        Task<T> DeleteAsync(int id);

        //Delete multi records
        Task DeleteMultiAsync(Expression<Func<T, bool>> where);

        // Get an entity by int id
        Task<T> GetSingleByIdAsync(int id);

        Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression, string[] includes = null);

        Task<IEnumerable<T>> GetAllAsync(string[] includes = null);

        Task<IEnumerable<T>> GetMultiAsync(Expression<Func<T, bool>> predicate, string[] includes = null);

        /// <summary>
        /// Get records by page.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="index"></param>
        /// <param name="size"></param>
        /// <param name="includes"></param>
        /// <returns>
        ///  Just have twos items
        ///  First index: All records match with keyword and pagging
        ///  Last index: Total record without pagging 
        ///  </returns>
        Task<object[]> GetMultiPagingAsync(Expression<Func<T, bool>> filter, int index = 0, int size = 50, string[] includes = null);

        Task<int> CountAsync(Expression<Func<T, bool>> where);

        Task<bool> CheckContainsAsync(Expression<Func<T, bool>> predicate);
    }
}
