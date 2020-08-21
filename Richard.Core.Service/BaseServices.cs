using Richard.Core.IRepository;
using Richard.Core.IService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Richard.Core.Service
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        public IBaseRepository<TEntity> BaseDal;
        

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await BaseDal.Query(whereExpression);
        }

        public async Task<int> Add(TEntity model)
        {
            return await BaseDal.Add(model);
        }
    }
}
