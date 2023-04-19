using IRAT.Inject.Model;
using IRAT.Inject.Model.Shared;
using IRAT.Inject.Support;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace IRAT.Inject.Business
{
    public class _Permission
    {
        public PermissionRf permissionRf;

        private static IRAT.Inject.Business._Log _log;

        public _Permission()
        {
            _log = new IRAT.Inject.Business._Log();
        }

        public IEnumerable<Permission> RetrieveAllPermissions(Expression<Func<Permission, bool>> predicate, params Expression<Func<Permission, object>>[] includeProperties)
        {
            using (Context context = new Context())
            {
                IQueryable<Permission> queryable = (from p in context.Permissions
                                                    select (p)).NullableWhere(predicate);
                if (includeProperties != null)
                {
                    queryable = includeProperties.Aggregate(queryable, (IQueryable<Permission> current, Expression<Func<Permission, object>> includeProperty) => current.Include(includeProperty));
                }
                return queryable.ToList();
            }
        }

        public IEnumerable<PermissionGroup> RetrieveAllPermissionGroups(Expression<Func<PermissionGroup, bool>> predicate, params Expression<Func<PermissionGroup, object>>[] includeProperties)
        {
            using (Context context = new Context())
            {
                IQueryable<PermissionGroup> queryable = (from p in context.PermissionGroups
                                                         select (p)).NullableWhere(predicate);
                if (includeProperties != null)
                {
                    queryable = includeProperties.Aggregate(queryable, (IQueryable<PermissionGroup> current, Expression<Func<PermissionGroup, object>> includeProperty) => current.Include(includeProperty));
                }
                return queryable.ToList();
            }
        }

        public OperationStatus InsertOrUpdate(PermissionGroup permissionGroup)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            try
            {
                if (permissionGroup == null)
                {
                    operationStatus.Status = false;
                    operationStatus.Message = "Oops, plese enter permission group.";
                    return result;
                }
                using (Context context = new Context())
                {
                    if (permissionGroup.PermissionGroupId == 0)
                    {
                        context.PermissionGroups.Add(permissionGroup);
                    }
                    else
                    {
                        context.Entry<PermissionGroup>(permissionGroup).State = EntityState.Modified;
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //Log log = new Log { UserId = _User.CurrentUser.UserId, LoglevelId = (int)_Enums.LogLevel.Error, Shortmessage = ex.Message, Fullmessage = OperationStatus.CreateException(ex), DateCreated = DateTime.Now };
                //_log.InsertOrUpdate(log);
            }
            return result;
        }

        public OperationStatus DeletePermissionGroup(PermissionGroup permissionGroup)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            try
            {
                if (permissionGroup.PermissionGroupId == 0)
                {
                    operationStatus.Status = false;
                    operationStatus.Message = "Oops, plese select a permission group.";
                    return result;
                }
                using (Context context = new Context())
                {
                    bool permissionRf = context.PermissionRfs.Where(x => x.PermissionGroupId == permissionGroup.PermissionGroupId).Any();
                    if (permissionRf)
                    {
                        operationStatus.Status = false;
                        operationStatus.Message = "Cannot delete a permission group that has asigned permission(s)";
                        return result;
                    }
                    PermissionGroup entity = context.PermissionGroups.Find(permissionGroup.PermissionGroupId);
                    if (entity != null)
                    {
                        context.PermissionGroups.Remove(entity);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                result = OperationStatus.CreateFromException("An error occurred", ex);
                //Log log = new Log { UserId = _User.CurrentUser.UserId, LoglevelId = (int)_Enums.LogLevel.Error, Shortmessage = ex.Message, Fullmessage = OperationStatus.CreateException(ex), DateCreated = DateTime.Now };
                //_log.InsertOrUpdate(log);
            }
            return result;
        }

        public OperationStatus InsertOrUpdate(List<PermissionRf> permission)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            permissionRf = new PermissionRf();
            try
            {
                using (Context context = new Context())
                {
                    if (permission != null)
                    {
                        foreach (PermissionRf item in permission)
                        {
                            if (item.PermissionGroupId < 0)
                            {
                                operationStatus.Status = false;
                                operationStatus.Message = "Oops, please select permission group.";
                                return result;
                            }
                            List<PermissionRf> permissionassigned = context.PermissionRfs.Where(i => i.PermissionGroupId == item.PermissionGroupId).ToList();
                            if (permissionassigned.Count() > 0)
                            {
                                foreach (PermissionRf permassigned in permissionassigned)
                                {
                                    PermissionRf entity = context.PermissionRfs.Find(permassigned.PermissionRfId);
                                    entity.Deleted = true;
                                    //context.PermissionRfs.Remove(entity);
                                    context.Entry<PermissionRf>(entity).State = EntityState.Modified;
                                    context.SaveChanges();
                                }
                            }
                        }
                        foreach (PermissionRf item in permission)
                        {
                            PermissionRf _permissionRf = context.PermissionRfs.Where(i => i.PermissionGroupId == item.PermissionGroupId && i.PermissionId == item.PermissionId && i.Deleted == false).FirstOrDefault();
                            if (_permissionRf == null)
                            {
                                permissionRf.PermissionGroupId = (int)item.PermissionGroupId;
                                permissionRf.PermissionId = item.PermissionId;
                                permissionRf.UserId = _User.CurrentUser.UserId;
                                permissionRf.DateCreated = DateTime.Now;
                                permissionRf.Deleted = false;
                                context.PermissionRfs.Add(permissionRf);
                            }
                            else
                            {
                                PermissionRf p = context.PermissionRfs.Find(_permissionRf.PermissionRfId);
                                p.PermissionGroupId = (int)item.PermissionGroupId;
                                p.PermissionId = item.PermissionId;
                                p.UserId = _User.CurrentUser.UserId;
                                p.DateCreated = DateTime.Now;
                                p.Deleted = false;
                                context.Entry<PermissionRf>(p).State = EntityState.Modified;
                            }
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = OperationStatus.CreateFromException("An error occurred", ex);
                Log log = new Log { UserId = _User.CurrentUser.UserId, LoglevelId = (int)_Enums.LogLevel.Error, Shortmessage = ex.Message, Fullmessage = OperationStatus.CreateException(ex), DateCreated = DateTime.Now };
                _log.InsertOrUpdate(log);
            }
            return result;
        }
    }
}
