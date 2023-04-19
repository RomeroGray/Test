using IRAT.Inject.Model;
using IRAT.Inject.Model.Shared;
using IRAT.Inject.Support;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IRAT.Inject.Business
{
    public class _Source
    {
        public IEnumerable<MatchTable> RetrieveAllSources(Expression<Func<MatchTable, bool>> predicate, params Expression<Func<MatchTable, object>>[] includeProperties)
        {
            using (Context context = new Context())
            {
                IQueryable<MatchTable> queryable = (from p in context.MatchTables
                                                 select (p)).NullableWhere(predicate);
                if (includeProperties != null)
                {
                    queryable = includeProperties.Aggregate(queryable, (IQueryable<MatchTable> current, Expression<Func<MatchTable, object>> includeProperty) => current.Include(includeProperty));
                }
                return queryable.ToList();
            }
        }

        public IEnumerable<MatchTable> Search(string keyword, string supplier, string daterange, bool? chkbxdate)
        {
            ObservableCollection<MatchTable> result = null;
            string[] array = null;
            DateTime? startDate = null;
            DateTime? endDate = null;
            Expression<Func<MatchTable, bool>> expr = PredicateBuilder.True<MatchTable>();
            using (Context context = new Context())
            {
                if (!string.IsNullOrEmpty(daterange) && chkbxdate == true)
                {
                    array = daterange.Split(new string[1] { "-" }, 2, StringSplitOptions.RemoveEmptyEntries);
                    startDate = Convert.ToDateTime(array[0]);
                    endDate = Convert.ToDateTime(array[1]);
                    expr = expr.And((MatchTable p) => (DateTime)DbFunctions.TruncateTime((DateTime?)p.DateCreated) >= (DateTime)(DateTime?)startDate && (DateTime)DbFunctions.TruncateTime((DateTime?)p.DateCreated) <= (DateTime)(DateTime?)endDate);
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    expr = expr.And((MatchTable p) => p.Name.Contains(keyword));
                }
                if (!string.IsNullOrEmpty(supplier))
                {
                    expr = expr.And((MatchTable p) => p.Supplier.Contains(supplier));
                }
                else
                {
                    expr = expr.And((MatchTable p) => p.Deleted == false);
                }
                result = new ObservableCollection<MatchTable>(context.MatchTables.Include(i => i.Tier).Include(i => i.Country).Include(i => i.User).AsExpandable().Where(expr));
            }
            return result;
        }

        public OperationStatus InsertOrUpdate(MatchTable matchTable)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            try
            {
                using (Context context = new Context())
                {
                    if (matchTable.MatchTableId == 0)
                    {
                        matchTable.Gid = _Helper.GuId(_Enums.GuId.Long);
                        matchTable.DateCreated = DateTime.Now;
                        matchTable.UserId = _User.CurrentUser.UserId;
                        context.MatchTables.Add(matchTable);
                        if (context.SaveChanges() > 0)
                        {
                            operationStatus.Message = "Save successfully";
                        }
                        else
                        {
                            operationStatus.Message = "Sorry, looks like there are some errors detected, please try again.";
                        }
                    }
                    else
                    {
                        matchTable.DateCreated = DateTime.Now;
                        matchTable.UserId = _User.CurrentUser.UserId;
                        context.Entry<MatchTable>(matchTable).State = EntityState.Modified;
                        if (context.SaveChanges() > 0)
                        {
                            operationStatus.Message = "Source updated successfully";
                        }
                        else
                        {
                            operationStatus.Message = "Sorry, looks like there are some errors detected, please try again.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = OperationStatus.GetOperationError("An error occurred", ex);
            }
            return result;
        }
    }
}
