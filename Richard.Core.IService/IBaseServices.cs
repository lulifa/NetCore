using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Richard.Core.IService
{
    public interface IBaseServices<TEntity> where TEntity : class
    {
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<int> Add(TEntity model);
    }
}
