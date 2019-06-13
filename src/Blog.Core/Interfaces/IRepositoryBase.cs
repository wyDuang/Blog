using Blog.Core.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
    public interface IRepositoryBase<T> where T : IEntity
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includes);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> whereLambd);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> whereLambd, params Expression<Func<T, object>>[] includes);

        IEnumerable<T> GetAllList();
        Task<List<T>> GetAllListAsync();

        IEnumerable<T> GetList(Expression<Func<T, bool>> whereLambd);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereLambd);
        IEnumerable<T> GetList(Expression<Func<T, bool>> whereLambd, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereLambd, params Expression<Func<T, object>>[] includes);

        Task<int> GetCountAsync();
        Task<int> GetCountAsync(Expression<Func<T, bool>> whereLambd);

        void Add(T entity, bool autoSave = false);
        void Update(T entity, bool autoSave = false);
        void Delete(T entity, bool autoSave = false);
        void DeleteWhere(Expression<Func<T, bool>> whereLambd, bool autoSave = false);
        void AddRange(IEnumerable<T> entities, bool autoSave = false);
        void DeleteRange(IEnumerable<T> entities, bool autoSave = false);

        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        void Detach(T entity);
        void DetachRange(IEnumerable<T> entities);
        void AttachAsModified(T entity);

        void Save();
        Task SaveAsync();

        Task<PaginatedList<T>> GetPageListAsync(PaginationBase parameters, IPropertyMapping propertyMapping);
        Task<PaginatedList<T>> GetPageListAsync(PaginationBase parameters, IPropertyMapping propertyMapping, Expression<Func<T, bool>> whereLambd);
        Task<PaginatedList<T>> GetPageListAsync(PaginationBase parameters, IPropertyMapping propertyMapping, Expression<Func<T, bool>> whereLambd, params Expression<Func<T, object>>[] includes);
    }
}
