using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IRAT.Inject.Support
{
    public static class IQueryableExtensions
    {
        public static bool ReduceExpressionInclusiveDefault
        {
            get;
            set;
        }

        public static IQueryable<T> NullableWhere<T>(this IQueryable<T> entity, Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
            {
                return entity;
            }
            return entity.Where(predicate);
        }

        static IQueryableExtensions()
        {
            ReduceExpressionInclusiveDefault = true;
        }

        public static IQueryable<TResult> ReduceSelectExpression<TResult>(this IQueryable<TResult> query, IEnumerable<string> memberNames)
        {
            return ReduceSelectExpression(query, memberNames, ReduceExpressionInclusiveDefault);
        }

        public static IQueryable<TResult> ReduceSelectExpression<TResult>(this IQueryable<TResult> query, IEnumerable<string> memberNames, bool inclusive)
        {
            return ReduceSelectExpression(query, memberNames, inclusive, typeof(TResult));
        }

        public static IQueryable<TResult> ReduceSelectExpression<TResult>(this IQueryable<TResult> query, IEnumerable<string> memberNames, bool inclusive, Type reduceType)
        {
            ReduceSelectExpressionVisitor reduceSelectExpressionVisitor = new ReduceSelectExpressionVisitor(memberNames, inclusive, reduceType);
            Expression expression = reduceSelectExpressionVisitor.Visit(query.Expression);
            return query.Provider.CreateQuery<TResult>(expression);
        }
    }
}
