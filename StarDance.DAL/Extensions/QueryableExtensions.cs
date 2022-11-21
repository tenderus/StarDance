using System.Text;
using StarDance.Common.Helpers;
using StarDance.Domain;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;


namespace StarDance.DAL.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedResult<T>> CreatePaginatedResult<T>(this IQueryable<T> query, PagedRequest pagedRequest, CancellationToken cancellationToken)
            where T : Entity
    {
        query = query.ApplyFilters(pagedRequest);

            var total = query.Count();

	        query = query.Sort(pagedRequest);
            
            
            var projectionResult = query.Paginate(pagedRequest);
            
            var listResult = await projectionResult.ToListAsync(cancellationToken);

            return new PaginatedResult<T>()
            {
                Items = listResult,
                PageSize = pagedRequest.PageSize,
                PageIndex = pagedRequest.PageIndex,
                Total = total
            };
        }

        private static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            var entities = query.Skip(pagedRequest.PageIndex * pagedRequest.PageSize).Take(pagedRequest.PageSize);
            return entities;
        }

        private static IQueryable<T> Sort<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            if (!string.IsNullOrWhiteSpace(pagedRequest.ColumnNameForSorting))
            {
                query = query.OrderBy(pagedRequest.ColumnNameForSorting + " " + pagedRequest.SortDirection);
            }
            return query;
        }

        private static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, PagedRequest pagedRequest)
        {
            var predicate = new StringBuilder();
            var requestFilters = pagedRequest.RequestFilters;
            for (int i = 0; i < requestFilters.Filters.Count; i++)
            {
                if (i > 0)
                {
                    predicate.Append($" {requestFilters.LogicalOperators} ");
                }
                string expression = _expressions[requestFilters.Filters[i].Expression];
                predicate.Append(requestFilters.Filters[i].Path + expression.Replace("{i}", $"{i}"));
            }

            if (requestFilters.Filters.Any())
            {
                var propertyValues = requestFilters.Filters.Select(filter => filter.Value).ToArray();

                query = query.Where(predicate.ToString(), propertyValues);
            }

            return query;
        }


        private static readonly Dictionary<Expression, string> _expressions = new Dictionary<Expression, string>()
        {
            { Expression.Equals, "==@{i}" },
            { Expression.Contains, ".Contains(@{i})" },
            { Expression.LessOrEqual, "<=@{i}" },
            { Expression.More, ">@{i}" }
        };
}