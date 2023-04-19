using IRAT.Inject.Model;
using IRAT.Inject.Support;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IRAT.Inject.Business
{
    public class _Location
    {
        public IEnumerable<Location> RetrieveAllLocations(Expression<Func<Location, bool>> predicate, params Expression<Func<Location, object>>[] includeProperties)
        {
            using (Context context = new Context())
            {
                IQueryable<Location> queryable = (from p in context.Locations
                                                  select (p)).NullableWhere(predicate);
                if (includeProperties != null)
                {
                    queryable = includeProperties.Aggregate(queryable, (IQueryable<Location> current, Expression<Func<Location, object>> includeProperty) => current.Include(includeProperty));
                }
                return queryable.ToList();
            }
        }
    }
}
