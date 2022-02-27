using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestManagement.Core.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            var entities = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return entities;
        }

    }
}
