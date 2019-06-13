using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.IRepository
{
    public interface IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        #region 查
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereLambd);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> whereLambd);
        Task<long> LongCountAsync();
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> whereLambd);

        TEntity Get(TPrimaryKey id);
        Task<TEntity> GetAsync(TPrimaryKey id);
        Task<List<TEntity>> GetAllListAsync();
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> whereLambd);
        Task<PaginatedList<TEntity>> GetPageListAsync(PaginatedQuery parameters, IPropertyMapping propertyMapping);
        Task<PaginatedList<TEntity>> GetPageListAsync(PaginatedQuery parameters, IPropertyMapping propertyMapping, Expression<Func<TEntity, bool>> whereLambd);
        Task<PaginatedList<TEntity>> GetPageListAsync(PaginatedQuery parameters, IPropertyMapping propertyMapping, Expression<Func<TEntity, bool>> whereLambd,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereLambd);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> whereLambd);
        #endregion

        #region 增删改
        void Insert(TEntity entity, bool autoSave = false);
        Task InsertAsync(TEntity entity, bool autoSave = false);
        void Update(TEntity entity, bool autoSave = false);
        Task UpdateAsync(TEntity entity, bool autoSave = false);
        TEntity InsertOrUpdate(TPrimaryKey id, TEntity entity);
        Task<TEntity> InsertOrUpdateAsync(TPrimaryKey id, TEntity entity);
        void Delete(TEntity entity, bool autoSave = false);
        void Delete(TPrimaryKey id, bool autoSave = false);
        void Delete(Expression<Func<TEntity, bool>> whereLambd, bool autoSave = false);
        #endregion

        void Save();
        Task SaveAsync();
    }
}