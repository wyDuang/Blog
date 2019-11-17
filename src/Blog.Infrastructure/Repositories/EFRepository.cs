using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Infrastructure.Database;
using Blog.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class EFRepository<T> : IRepositoryBase<T> where T : Entity
    {
        protected readonly DbSet<T> dbSet;
        protected readonly MyContext Context;
        public EFRepository(MyContext context)
        {
            Context = context;
            dbSet = Context.Set<T>();
        }

        public virtual T Get(int id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(dbSet.AsQueryable(), (current, include) => current.Include(include));

            return await queryableResultWithIncludes.SingleOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> whereLambd)
        {
            return await dbSet.FirstOrDefaultAsync(whereLambd);
        }

        public virtual async Task<T> GetSingleAsync(Expression<Func<T, bool>> whereLambd, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(dbSet.AsQueryable(), (current, include) => current.Include(include));

            return await queryableResultWithIncludes.FirstOrDefaultAsync(whereLambd);
        }


        public virtual IEnumerable<T> GetAllList()
        {
            return dbSet.AsEnumerable();
        }

        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> whereLambd)
        {
            return dbSet.Where(whereLambd).AsEnumerable();
        }

        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> whereLambd, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(dbSet.AsQueryable(), (current, include) => current.Include(include));

            return queryableResultWithIncludes.Where(whereLambd).AsEnumerable();
        }

        public virtual async Task<List<T>> GetAllListAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereLambd)
        {
            return await dbSet.Where(whereLambd).ToListAsync();
        }

        public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereLambd, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(dbSet.AsQueryable(), (current, include) => current.Include(include));

            return await queryableResultWithIncludes.Where(whereLambd).ToListAsync();
        }

        public virtual async Task<int> GetCountAsync()
        {
            return await dbSet.CountAsync();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> whereLambd)
        {
            return await dbSet.CountAsync(whereLambd);
        }

        public void Add(T entity, bool autoSave = false)
        {
            dbSet.Add(entity);
            if (autoSave)
                Save();
        }

        public void Update(T entity, bool autoSave = false)
        {
            Context.Entry(entity).State = EntityState.Modified;
            if (autoSave)
                Save();
        }

        public void Delete(T entity, bool autoSave = false)
        {
            dbSet.Remove(entity);
            if (autoSave)
                Save();
        }

        public void DeleteWhere(Expression<Func<T, bool>> whereLambd, bool autoSave = false)
        {
            IEnumerable<T> entities = dbSet.Where(whereLambd);
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Deleted;
            }
            if (autoSave)
                Save();
        }

        public void AddRange(IEnumerable<T> entities, bool autoSave = false)
        {
            dbSet.AddRange(entities);
            if (autoSave)
                Save();
        }

        public void DeleteRange(IEnumerable<T> entities, bool autoSave = false)
        {
            entities.ToList().ForEach(entity => dbSet.Remove(entity));
            if (autoSave)
                Save();
        }


        public void Attach(T entity)
        {
            dbSet.Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Attach(entity);
            }
        }

        public void Detach(T entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        public void DetachRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Detach(entity);
            }
        }

        public void AttachAsModified(T entity)
        {
            Attach(entity);
            Update(entity);
        }

        /// <summary>
        /// 事务性保存
        /// </summary>
        public void Save()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// 事务性异步保存
        /// </summary>
        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual async Task<PaginatedList<T>> GetPageListAsync(PaginationBase parameters, IPropertyMapping propertyMapping)
        {
            var collectionBeforePaging = dbSet.ApplySort(parameters.OrderBy, propertyMapping);

            var count = await collectionBeforePaging.CountAsync();
            var items = await collectionBeforePaging
                .Skip(parameters.PageIndex * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var result = new PaginatedList<T>(parameters.PageIndex, parameters.PageSize, count, items);
            return result;
        }

        public virtual async Task<PaginatedList<T>> GetPageListAsync(PaginationBase parameters, IPropertyMapping propertyMapping, Expression<Func<T, bool>> whereLambd)
        {
            var collectionBeforePaging = dbSet.Where(whereLambd).ApplySort(parameters.OrderBy, propertyMapping);

            var count = await collectionBeforePaging.CountAsync();
            var items = await collectionBeforePaging
                .Skip(parameters.PageIndex * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var result = new PaginatedList<T>(parameters.PageIndex, parameters.PageSize, count, items);
            return result;
        }

        public virtual async Task<PaginatedList<T>> GetPageListAsync(PaginationBase parameters, IPropertyMapping propertyMapping, Expression<Func<T, bool>> whereLambd, params Expression<Func<T, object>>[] includes)
        {
            var collectionBeforePaging = includes.Aggregate(dbSet.Where(whereLambd).ApplySort(parameters.OrderBy, propertyMapping), (current, include) => current.Include(include));

            var count = await collectionBeforePaging.CountAsync();
            var items = await collectionBeforePaging
                .Skip(parameters.PageIndex * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var result = new PaginatedList<T>(parameters.PageIndex, parameters.PageSize, count, items);
            return result;
        }
    }
}
