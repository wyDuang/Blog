using Blog.Core.Interfaces;
using System.Collections.Generic;

namespace Blog.Core.BaseModels
{
    public class PaginatedList<T> : List<T> where T : IEntity
    {
        public PaginationBase Pagination { get; }

        private int _totalItemsCount;
        public int TotalItemsCount
        {
            get => _totalItemsCount;
            set => _totalItemsCount = value >= 0 ? value : 0;
        }

        public int PageCount => TotalItemsCount / Pagination.PageSize + (TotalItemsCount % Pagination.PageSize > 0 ? 1 : 0);

        public bool HasPrevious => Pagination.PageIndex > 0;
        public bool HasNext => Pagination.PageIndex < PageCount - 1;

        public PaginatedList(int pageIndex, int pageSize, int totalItemsCount, IEnumerable<T> data)
        {
            Pagination = new PaginationBase
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            TotalItemsCount = totalItemsCount;
            AddRange(data);
        }
    }
}
