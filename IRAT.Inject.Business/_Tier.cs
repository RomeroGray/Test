using IRAT.Inject.Model;
using IRAT.Inject.Support;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IRAT.Inject.Business
{
    public class _Tier
    {
        public IEnumerable<Tier> RetrieveAllTiers(Expression<Func<Tier, bool>> predicate, params Expression<Func<Tier, object>>[] includeProperties)
        {
            using (Context context = new Context())
            {
                IQueryable<Tier> queryable = (from p in context.Tiers
                                                  select (p)).NullableWhere(predicate);
                if (includeProperties != null)
                {
                    queryable = includeProperties.Aggregate(queryable, (IQueryable<Tier> current, Expression<Func<Tier, object>> includeProperty) => current.Include(includeProperty));
                }
                return queryable.ToList();
            }
        }
    }
}
