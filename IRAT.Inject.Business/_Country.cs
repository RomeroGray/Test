using IRAT.Inject.Model;
using IRAT.Inject.Support;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IRAT.Inject.Business
{
    public class _Country
    {
        public IEnumerable<Country> RetrieveAllCountries(Expression<Func<Country, bool>> predicate, params Expression<Func<Country, object>>[] includeProperties)
        {
            using (Context context = new Context())
            {
                IQueryable<Country> queryable = (from p in context.Countries
                                                  select (p)).NullableWhere(predicate);
                if (includeProperties != null)
                {
                    queryable = includeProperties.Aggregate(queryable, (IQueryable<Country> current, Expression<Func<Country, object>> includeProperty) => current.Include(includeProperty));
                }
                return queryable.ToList();
            }
        }
    }
}
