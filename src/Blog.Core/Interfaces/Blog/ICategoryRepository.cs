using Blog.Core.BaseModels;
using Blog.Core.Entities;
using Blog.Core.Interfaces;
using Blog.Core.QueryParameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {

    }
}
