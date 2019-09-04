using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WyBlog.Core.AutoMapper;
using WyBlog.Core.Exceptions;
using WyBlog.Core.Models;
using WyBlog.IRepository;
using WyBlog.Repository.MySql.Database;

namespace WyBlog.Repository.MySql.Repository
{
    public class BaseRepository<TEntity, TPrimaryKey> : IBaseRepository<TEntity, TPrimaryKey> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;
        public BlogDbContext _dbContext { get; } = null;
        //private readonly IPropertyMappingContainer _propertyMappingContainer;
        public BaseRepository(
            BlogDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereLambd)
        {
            return await _dbSet.Where(whereLambd).AnyAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereLambd)
        {
            return await _dbSet.Where(whereLambd).CountAsync();
        }

        public async Task<long> LongCountAsync()
        {
            return await _dbSet.LongCountAsync();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> whereLambd)
        {
            return await _dbSet.Where(whereLambd).LongCountAsync();
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> whereLambd)
        {
            return await _dbSet.Where(whereLambd).ToListAsync();
        }

        //public async Task<PaginatedList<TEntity>> GetPageListAsync(QueryParameters queryParameters)
        //{
        //    var query = _dbSet.AsQueryable();

        //    query = query.ApplySort(queryParameters.OrderBy, _propertyMappingContainer.Resolve<TSource, TEntity>());

        //    var count = await _dbSet.CountAsync();
        //    var data = await _dbSet
        //        .Skip(queryParameters.PageIndex * queryParameters.PageSize)
        //        .Take(queryParameters.PageSize)
        //        .ToListAsync();

        //    return new PaginatedList<TEntity>(queryParameters.PageIndex, queryParameters.PageSize, count, data);
        //}

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereLambd)
        {
            return await _dbSet.Where(whereLambd).FirstOrDefaultAsync();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> whereLambd)
        {
            return await _dbSet.Where(whereLambd).SingleAsync();
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public void Update(TEntity entity)
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
        }
    }
}
