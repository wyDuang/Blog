using System;
using System.Collections.Generic;
using System.Text;

namespace WyBlog.IRepository
{
    public interface IBaseRepository<TEntity, TPrimaryKey> : IDisposable where TEntity : class
    {

    }
}
