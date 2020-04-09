using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace INT1448.EntityFramework.EntityFramework.Infrastructure
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private INT1448DbContext dataContext;
        private  IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected INT1448DbContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region Implementation
        public async virtual Task<T> AddAsync(T entity)
        {
            Func<T> Add = () => {
                return dbSet.Add(entity);
            };
            return await Task.Run(Add);
        }

        public async virtual Task UpdateAsync(T entity)
        {
            Func<T> Update = () => {
                T entityUpdated = dbSet.Attach(entity);
                dataContext.Entry(entity).State = EntityState.Modified;
                return entityUpdated;
            };
            await Task.Run(Update);  
        }

        public async virtual Task<T> DeleteAsync(T entity)
        {
            Func<T> Delete = () => {
                return dbSet.Remove(entity);
            };
            return await Task.Run(Delete);
        }

        public async virtual Task<T> DeleteAsync(int id)
        {
            Func<T> Delete = () => {
                var entity = dbSet.Find(id);
                return dbSet.Remove(entity);
            };
            return await Task.Run(Delete);
        }

        public async virtual Task DeleteMultiAsync(Expression<Func<T, bool>> where)
        {
            Func<IEnumerable<T>> Delete = () => {
                IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
                foreach (T obj in objects)
                    dbSet.Remove(obj);
                return objects;
            };
            await Task.Run(Delete);
        }

        public async virtual Task<T> GetSingleByIdAsync(int id)
        {
            Func<T> Get = () => {
                return dbSet.Find(id);
            };
            return await Task.Run(Get);
        }

        public async virtual Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where, string includes)
        {
            Func<IEnumerable<T>> Get = () => {
                return dbSet.Where(where).ToList();
            };
            return await Task.Run(Get);
        }

        public async virtual Task<int> CountAsync(Expression<Func<T, bool>> where)
        {
            Func<int> Count = () => {
                return dbSet.Count(where);
            };
            return await Task.Run(Count);
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null)
        {
            Func<IEnumerable<T>> GetAllsync = () => {
                //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
                if (includes != null && includes.Count() > 0)
                {
                    var query = dataContext.Set<T>().Include(includes.First());
                    foreach (var include in includes.Skip(1))
                        query = query.Include(include);
                    return query.AsQueryable();
                }

                return dataContext.Set<T>().AsQueryable();
            };
            return await Task.Run(()=> GetAllsync());   
        }

        public async Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            Func<T> GetSingleByCondition = () => {
                if (includes != null && includes.Count() > 0)
                {
                    var query = dataContext.Set<T>().Include(includes.First());
                    foreach (var include in includes.Skip(1))
                        query = query.Include(include);
                    return query.FirstOrDefault(expression);
                }
                return dataContext.Set<T>().FirstOrDefault(expression);
            };
            return await Task.Run(GetSingleByCondition);
        }

        public async virtual Task<IEnumerable<T>> GetMultiAsync(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            Func<IEnumerable<T>> GetMulti = () => {
                //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
                if (includes != null && includes.Count() > 0)
                {
                    var query = dataContext.Set<T>().Include(includes.First());
                    foreach (var include in includes.Skip(1))
                        query = query.Include(include);
                    return query.Where<T>(predicate).AsQueryable<T>();
                }

                return dataContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
            };
            return await Task.Run(GetMulti);
        }

        public async virtual Task<object[]> GetMultiPagingAsync(Expression<Func<T, bool>> predicate, int index = 0, int size = 20, string[] includes = null)
        {
            Func<object[]> GetMultiPaging = () => {
                int skipCount = index * size;
                IQueryable<T> _resetSet;

                //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
                if (includes != null && includes.Count() > 0)
                {
                    var query = dataContext.Set<T>().Include(includes.First());
                    foreach (var include in includes.Skip(1))
                        query = query.Include(include);
                    _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
                }
                else
                {
                    _resetSet = predicate != null ? dataContext.Set<T>().Where<T>(predicate).AsQueryable() : dataContext.Set<T>().AsQueryable();
                }

                _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
                int total = _resetSet.Count();
                return new object[] { _resetSet.AsQueryable(), total};
            };
            return await Task.Run(GetMultiPaging);
        }

        public async Task<bool> CheckContainsAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(()=> { return dataContext.Set<T>().Count<T>(predicate) > 0; });
        }
        #endregion
    }
}
