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
using System.Web;

namespace IRAT.Inject.Business
{
    public class _Log
    {
        public IEnumerable<Log> RetrieveAllLogs(Expression<Func<Log, bool>> predicate, params Expression<Func<Log, object>>[] includeProperties)
        {
            using (Context context = new Context())
            {
                IQueryable<Log> queryable = (from p in context.Logs
                                             select (p)).NullableWhere(predicate);
                if (includeProperties != null)
                {
                    queryable = includeProperties.Aggregate(queryable, (IQueryable<Log> current, Expression<Func<Log, object>> includeProperty) => current.Include(includeProperty));
                }
                return queryable.ToList();
            }
        }

        public IEnumerable<Log> Search(int? LevelId, string daterange)
        {
            ObservableCollection<Log> Logs = null;
            string[] array = null;
            DateTime? startDate = null;
            DateTime? endDate = null;
            Expression<Func<Log, bool>> expr = PredicateBuilder.True<Log>();
            using (Context context = new Context())
            {
                if (!string.IsNullOrEmpty(daterange))
                {
                    array = daterange.Split(new string[1] { "-" }, 2, StringSplitOptions.RemoveEmptyEntries);
                    startDate = Convert.ToDateTime(array[0]);
                    endDate = Convert.ToDateTime(array[1]);
                    expr = expr.And((Log p) => (DateTime)DbFunctions.TruncateTime((DateTime?)p.DateCreated) >= (DateTime)(DateTime?)startDate && (DateTime)DbFunctions.TruncateTime((DateTime?)p.DateCreated) <= (DateTime)(DateTime?)endDate);
                }
                if (LevelId > 0)
                {
                    expr = expr.And((Log p) => (int)p.LoglevelId == LevelId);
                }
                Logs = new ObservableCollection<Log>(context.Logs.Include(i => i.User).AsExpandable().Where(expr));

            }
            return Logs;
        }

        public Log InsertOrUpdate(Log Log)
        {
            try
            {
                using (Context context = new Context())
                {
                    if (Log.Id == 0)
                    {
                        HttpRequest request = HttpContext.Current.Request;
                        if (request != null)
                        {
                            Log.PageURL = request.Url == null ? "" : request.Url.ToString();
                            Log.ReferrerURL = request.UrlReferrer == null ? "" : request.UrlReferrer.ToString();
                        }
                        Log.Gid = _Helper.GuId(_Enums.GuId.small);
                        Log.IPaddress = _Helper.GetIPAddress() == null ? "" : _Helper.GetIPAddress();
                        //Log.LocationId = _User.CurrentUser.LocationId;
                        Log.DateCreated = DateTime.Now;
                        context.Logs.Add(Log);
                        context.SaveChanges();
                    }
                }
            }
            catch
            { }
            return null;
        }

        public OperationStatus Clear()
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            try
            {
                using (Context context = new Context())
                {
                    var list = context.Logs.ToList();
                    foreach (Log l in list)
                    {
                        Log entity = context.Logs.Find(l.Id);
                        context.Logs.Remove(entity);
                        if (context.SaveChanges() > 0)
                        {
                            operationStatus.Message = "Log delete successfully";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = OperationStatus.CreateFromException("An error occurred", ex);
                //_log.InsertOrUpdate(new Log { LoglevelId = (int)_Enums.LogLevel.Error, Shortmessage = ex.Message, Fullmessage = OperationStatus.CreateException(ex), DateCreated = DateTime.Now });
            }
            return result;
        }
    }
}
