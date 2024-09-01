

using Microsoft.EntityFrameworkCore;

namespace ChatApp.Contract.Abstractions.Shared
{
    public class PageResult<T>
    {
        public const int UpperPageSize = 100;
        public const int DefaultPageSie = 10;
        public const int DefaultPageIndex = 1;
        public List<T> Items { get; }
        public int PageIndex { get; }
        public int TotalCount { get; }
        public int PageSize { get; }
        public bool HasNextPage => PageIndex * PageSize < TotalCount;
        public bool HasPreviousPage => PageIndex > 1;
        private PageResult(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        public static async Task<PageResult<T>> CreateAsync(IQueryable<T> query, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex <= 0 ? DefaultPageIndex : pageIndex;
            pageSize = pageSize <= 0 ?
                DefaultPageSie : pageSize > UpperPageSize ? UpperPageSize : pageSize;
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new(items, pageIndex, pageSize, totalCount);
        }
        public static PageResult<T> Create(List<T> items, int pageIndex, int pageSize, int totalCount) => new(items, pageIndex, pageSize, totalCount);
    }
}
