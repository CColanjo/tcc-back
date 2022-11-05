

using AutoMapper;
using schedule_appointment_domain.Model.Pagination;
using schedule_appointment_domain.Model.Pagination.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace schedule_appointment_infra.Extensions
{
    public static class LinqExtensions
    {
        public static async Task<Page<TContent>> PageAsync<TEntity, TContent>(this IQueryable<TEntity> query,
                                        IPageable pageable,
                                        IMapper mapper
                                        ) where TEntity : class
        {
            var page = new Page<TContent>
            {
                Pageable =
                {
                    PageNumber = pageable.PageNumber,
                    PageSize = pageable.PageSize
                },
                TotalRecords = query.Count()
            };

            var pageCount = (double)page.TotalRecords / pageable.PageSize;
            page.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = (pageable.PageNumber - 1) * pageable.PageSize;
            
            var content = await query
                .Skip(skip)
                .Take(pageable.PageSize)
                .ToListAsync();

            page.Content = mapper.Map<IList<TContent>>(content);

            return page;
        }

        public static async Task<Page<T>> PageAsync<T>(this IQueryable<T> query, IPageable pageable) where T : class
        {
            var page = new Page<T>
            {
                Pageable =
                {
                    PageNumber = pageable.PageNumber,
                    PageSize = pageable.PageSize
                },
                TotalRecords = query.Count()
            };

            var pageCount = (double)page.TotalRecords / pageable.PageSize;
            page.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = (pageable.PageNumber - 1) * pageable.PageSize;
            var content = await query
                .Skip(skip)
                .Take(pageable.PageSize)
                .ToListAsync();

            page.Content = content;

            return page;
        }

        public static Page<T> Page<T>(this IEnumerable<T> query, IPageable pageable) where T : class
        {
            var page = new Page<T>
            {
                Pageable =
                {
                    PageNumber = pageable.PageNumber,
                    PageSize = pageable.PageSize
                },
                TotalRecords = query.Count()
            };

            var pageCount = (double)page.TotalRecords / pageable.PageSize;
            page.TotalPages = (int)Math.Ceiling(pageCount);

            var skip = (pageable.PageNumber - 1) * pageable.PageSize;
            var content = query
                .Skip(skip)
                .Take(pageable.PageSize)
                .ToList();

            page.Content = content;

            return page;
        }
    }
}
