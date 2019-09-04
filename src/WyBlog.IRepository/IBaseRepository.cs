using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WyBlog.IRepository
{
    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        #region 查
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereLambd);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereLambd);
        Task<long> LongCountAsync();
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> whereLambd);
        Task<TEntity> GetAsync(TPrimaryKey id);
        Task<List<TEntity>> GetAllListAsync();
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> whereLambd);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereLambd);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> whereLambd);
        #endregion

        #region 增删改
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        #endregion
    }
}
