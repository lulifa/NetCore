using Richard.Core.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Richard.Core.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private ISqlSugarClient Db;
        private IUnitOfWork UnitOfWork;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            Db = unitOfWork.GetSugarClient();
            UnitOfWork = unitOfWork;
        }

        public async Task<int> Add(TEntity model)
        {
            var insert = Db.Insertable(model);
            return await insert.ExecuteReturnIdentityAsync();
        }

        public async Task<int> Add(List<TEntity> listEntity)
        {
            return await Db.Insertable(listEntity).ExecuteCommandAsync();
        }

        public async Task<bool> Delete(TEntity model)
        {
            return await Db.Deleteable(model).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> DeleteById(object id)
        {
            return await Db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await Db.Deleteable<TEntity>().In(ids).ExecuteCommandHasChangeAsync();
        }

        public async Task<List<TEntity>> Query()
        {
            return await Db.Queryable<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
        }

        public async Task<TEntity> QueryById(object objId)
        {
            return await Db.Queryable<TEntity>().In(objId).SingleAsync();
        }

        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await Db.Queryable<TEntity>().WithCacheIF(blnUseCache).In(objId).SingleAsync();
        }

        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await Db.Queryable<TEntity>().In(lstIds).ToListAsync();
        }

        public async Task<bool> Update(TEntity model)
        {
            return await Db.Updateable(model).ExecuteCommandHasChangeAsync();
        }
    }
}
