using AutoMapper;
using INT1448.Application.IServices;
using INT1448.EntityFramework.EntityFramework.Infrastructure;
using INT1448.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace INT1448.Application.Services
{
    public class ConvertToViewModel<TSource, TDest> : RepositoryBase<TSource>, IConvertToViewModel<TSource, TDest> 
        where TSource : class 
        where TDest : class
    {
        private readonly IMapper _mapper;
        protected ConvertToViewModel(IDbFactory dbFactory, IMapper mapper) 
            : base(dbFactory)
        {
            this._mapper = mapper;
        }

        public async Task<IEnumerable<TDest>> GetAllToView()
        {
            Func<Task<IEnumerable<TDest>>> GetAll = async () => {

                IEnumerable<TSource> itemFound = await GetAllAsync();

                IEnumerable<TDest> bookDTOs = itemFound.ForEach<TSource, TDest>((item) => {
                    return _mapper.Map<TSource, TDest>(item);
                });
                return bookDTOs;
            };

            return await Task.Run(GetAll);
        }

        public async Task<TDest> GetByIdToView(int id)
        {
            Func<Task<TDest>> Get = async () =>
            {
                TSource itemFound = await this.DbContext.Set<TSource>().FindAsync(id);

                return _mapper.Map<TSource, TDest>(itemFound);
            };

            return await Task.Run(Get);
        }
    }
}
