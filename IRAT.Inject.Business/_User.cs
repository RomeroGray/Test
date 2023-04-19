using System;
using System.Linq;
using IRAT.Inject.Model;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using LinqKit;
using System.Data.Entity;
using static IRAT.Inject.Business._Enums;
using System.Web;
using System.Web.Security;
using IRAT.Inject.UIModel;
using System.Threading;
using IRAT.Inject.Support;
using IRAT.Inject.Model.Shared;

namespace IRAT.Inject.Business
{
    public class _User
    {
        private readonly IRAT.Inject.Business._Log _log;
        private readonly IRAT.Inject.Business._Audit _audit;
        //private readonly IRAT.Inject.Business._Settings _settings;
        //private readonly IRAT.Inject.Business._SMTPEmail _smtpemailLogic;
        //private readonly IRAT.Inject.Business._MailBuilderUser _emailBuilderUser;

        public _User()
        {
            this._log = new IRAT.Inject.Business._Log();
            this._audit = new IRAT.Inject.Business._Audit();
            //this._settings = new IRAT.Inject.Business._Settings();
            //this._smtpemailLogic = new IRAT.Inject.Business._SMTPEmail();
            //this._emailBuilderUser = new IRAT.Inject.Business._MailBuilderUser();
        }

        public IEnumerable<Model.User> RetrieveAllUsers(Expression<Func<Model.User, bool>> predicate, params Expression<Func<Model.User, object>>[] includeProperties)
        {
            using (Context context = new Context())
            {
                IQueryable<Model.User> queryable = (from p in context.Users
                                                    select (p)).NullableWhere(predicate);
                if (includeProperties != null)
                {
                    queryable = includeProperties.Aggregate(queryable, (IQueryable<Model.User> current, Expression<Func<Model.User, object>> includeProperty) => current.Include(includeProperty));
                }
                return queryable.ToList();
            }
        }

        public IEnumerable<User> Search(string keyword, int? companyId, int? pg, string daterange, bool? chkbxdate)
        {
            ObservableCollection<User> result = null;
            string[] array = null;
            DateTime? startDate = null;
            DateTime? endDate = null;
            Expression<Func<User, bool>> expr = PredicateBuilder.True<User>();
            using (Context context = new Context())
            {
                if (!string.IsNullOrEmpty(daterange) && chkbxdate == true)
                {
                    array = daterange.Split(new string[1] { "-" }, 2, StringSplitOptions.RemoveEmptyEntries);
                    startDate = Convert.ToDateTime(array[0]);
                    endDate = Convert.ToDateTime(array[1]);
                    expr = expr.And((User p) => (DateTime)DbFunctions.TruncateTime((DateTime?)p.DateCreated) >= (DateTime)(DateTime?)startDate && (DateTime)DbFunctions.TruncateTime((DateTime?)p.DateCreated) <= (DateTime)(DateTime?)endDate);
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    expr = expr.And((User p) => p.Firstname.Contains(keyword));
                }
                if (pg > 0)
                {
                    expr = expr.And((User p) => p.PermissionGroupId == pg);
                }
                else
                {
                    expr = expr.And((User p) => p.Deleted == false);
                }
                result = new ObservableCollection<User>(context.Users.Include(i => i.PermissionGroup).AsExpandable().Where(expr));
            }
            return result;
        }

        public virtual UserLoginResults ValidateUser(string username, string password)
        {
            Model.User user = null;

            using (Context context = new Context())
            {
                var paswd = _Helper.Sha5121Encode(password);

                var u = context.Users.SingleOrDefault((Model.User i) => i.Username == username);
                if (u != null)
                    user = context.Users.Find(u.UserId);
                if (user == null)
                {
                    _audit.Insert(new AuditTrail { UserId = (u == null) ? (int)1 : u.UserId, AuditTypeId = (int)_Enums.AuditType.Security, Description = "An account failed to log on :" + " User " + username + " | " + _Audit.DetectDevice });
                    return UserLoginResults.UserNotExist;
                }
                if (user.Deleted == true)
                    return UserLoginResults.Deleted;
                if (user.Status == false)
                    return UserLoginResults.NotActive;
                if (user.Islocked == true)
                    return UserLoginResults.LockedOut;
                if (user.FailedLoginAttempts == 10/*_settings.PasswordaAllowedAttempts*/)
                {
                    user.Islocked = true;
                    user.LastlockedDate = DateTime.Now;
                    _audit.Insert(new Model.AuditTrail { UserId = u.UserId, AuditTypeId = (int)_Enums.AuditType.Security, Description = "Account has been locked at " + user.LastlockedDate + " : " + user.Firstname + " " + user.Lastname });
                    context.SaveChanges();
                    _audit.Insert(new AuditTrail { UserId = (u == null) ? (int?)null : u.UserId, AuditTypeId = (int)_Enums.AuditType.Security, Description = "Account has been locked invalid attempts:" + " User " + username + " | " + _Audit.DetectDevice });
                    return UserLoginResults.LockedOut;
                }
                if (!PasswordsMatch(user.UserId, paswd))
                {   //wrong password
                    {
                        _audit.Insert(new Model.AuditTrail { UserId = u.UserId, AuditTypeId = (int)_Enums.AuditType.Security, Description = "Log on attempt failed, incorrect password : " + user.Firstname + " " + user.Lastname });
                        user.FailedLoginAttempts++;
                        context.SaveChanges();
                    }
                    _audit.Insert(new AuditTrail { UserId = (u == null) ? (int?)null : u.UserId, AuditTypeId = (int)_Enums.AuditType.Security, Description = "A logon was attempted using explicit credentials :" + " User " + username + " | " + _Audit.DetectDevice });
                    return UserLoginResults.WrongPassword;
                }
                //update login details
                //user.Email = user.Email;
                user.Islocked = false;
                user.LastlockedDate = null;
                user.LoginStatus = true;
                user.FailedLoginAttempts = 0;
                user.LastDateLogin = DateTime.Now;
                user.LastPasswordChangedDate = null;
                context.SaveChanges();
                //AuditString
                _audit.Insert(new AuditTrail { UserId = u.UserId, AuditTypeId = (int)_Enums.AuditType.Security, Description = "An account was successfully logged on: User -" + u.Firstname + " " + u.Lastname + " | " + _Audit.DetectDevice });
                return UserLoginResults.Successful;
            }
        }

        public bool PasswordsMatch(int ID, string password)
        {
            using (Context context = new Context())
            {
                return context.Users.Where(u => u.UserId == ID && u.Password == password).Any();
            }
        }

        public OperationStatus LogOff()
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            try
            {
                using (Context context = new Context())
                {
                    Model.User user = context.Users.Find(_User.CurrentUser.UserId);
                    user.LoginStatus = false;
                    context.SaveChanges();
                    if (user != null)
                    {
                        _audit.Insert(new AuditTrail { AuditTypeId = (int)_Enums.AuditType.Security, Description = "An account was logged off: User -" + _User.CurrentUser.Username + " | " + _Audit.DetectDevice });
                        result.Message = "User logout successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                result = OperationStatus.CreateFromException("An error occurred", ex);
            }
            return result;
        }

        public static User CurrentUser
        {
            get
            {
                Model.User user = null;
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                try
                {
                    if (authCookie != null)
                    {
                        var cookieValue = authCookie.Value;
                        if (!String.IsNullOrWhiteSpace(cookieValue))
                        {
                            var ticket = FormsAuthentication.Decrypt(cookieValue);

                            int Id = Convert.ToInt32(ticket.Name);
                            using (Context context = new Context())
                            {
                                user = context.Users.Include(i => i.PermissionGroup).FirstOrDefault((Model.User u) => u.UserId == Id);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    FormsAuthentication.SignOut();
                    //throw new Exception("Error locatinng current login user. " + ex.Message);
                }
                return user;
            }
        }

        public static int CurrentID
        {
            get
            {
                return CurrentUser.UserId;
            }
        }

        public static string Currentuserfullname
        {
            get
            {
                return CurrentUser.Firstname + " " + CurrentUser.Lastname;
            }
        }

        public string GetUserById(int Id)
        {
            User user = Enumerable.FirstOrDefault(RetrieveAllUsers((User i) => i.UserId == Id && i.Deleted == false));
            return user.Firstname + " " + user.Lastname;
        }

        public User GetUser(int userId)
        {
            return Enumerable.FirstOrDefault(RetrieveAllUsers((User i) => i.UserId == userId && i.Deleted == false, (User i) => i.PermissionGroup));
        }

        public User GetUser(string username)
        {
            using (Context context = new Context())
            {
                return Enumerable.FirstOrDefault(RetrieveAllUsers((User i) => i.Username == username && i.Deleted == false, (User i) => i.PermissionGroup));
            }
        }

        public User GetUser(string username, string password)
        {
            using (Context context = new Context())
            {
                return Enumerable.FirstOrDefault(RetrieveAllUsers((User i) => i.Username == username && i.Password == password && i.Deleted == false, (User i) => i.PermissionGroup));
            }
        }

        public User GetUserByUsername(string username)
        {
            using (Context context = new Context())
            {
                return Enumerable.FirstOrDefault(RetrieveAllUsers((User i) => i.Username == username && i.Deleted == false, (User i) => i.PermissionGroup));
            }
        }

        public User GetUserByEmail(string email, int companyId)
        {
            using (Context context = new Context())
            {
                return Enumerable.FirstOrDefault(RetrieveAllUsers((User i) => i.Email == email && i.Deleted == false, (User i) => i.PermissionGroup));
            }
        }

        public OperationStatus InsertOrUpdate(User user)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            string password = user.Password;
            try
            {
                using (Context context = new Context())
                {
                    if (user.UserId == 0)
                    {
                        bool useremail = Enumerable.Any(RetrieveAllUsers((User i) => i.Email == user.Email && i.Deleted != true, (User i) => i.PermissionGroup));
                        if (useremail)
                        {
                            operationStatus.Status = false;
                            operationStatus.Message = "Email address already exists in database.";
                            return result;
                        }
                        user.Username = user.Firstname + "." + user.Lastname;
                        user.Password = _Helper.Sha5121Encode(user.Password);
                        user.Gid = _Helper.GuId(_Enums.GuId.Long);
                        user.Status = true;
                        user.Islocked = false;
                        user.LoginStatus = false;
                        user.FailedLoginAttempts = 0;
                        user.DateCreated = DateTime.Now;
                        context.Users.Add(user);
                        if (context.SaveChanges() > 0)
                        {
                            operationStatus.Message = user.Firstname + " " + user.Lastname + " save successfully";
                            //SendEmailInBackgroundThread(user, password, string.IsNullOrEmpty(_settings.Appsettingshosturl) ? "https://" : _settings.Appsettingshosturl, "AccountCreated", "IRAT AMS Account Creation");
                        }
                    }
                    else
                    {
                        user.Username = user.Firstname + "." + user.Lastname;
                        context.Entry<Model.User>(user).State = EntityState.Modified;
                        context.SaveChanges();
                        operationStatus.Message = user.Firstname + " " + user.Lastname + " updated successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                _log.InsertOrUpdate(new Log { LoglevelId = (int)_Enums.LogLevel.Error, Shortmessage = ex.Message, Fullmessage = OperationStatus.CreateException(ex), DateCreated = DateTime.Now });
            }
            return result;
        }

        public OperationStatus ResetPassword(User model)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            try
            {
                using (Context context = new Context())
                {
                    var u = Enumerable.SingleOrDefault(RetrieveAllUsers((User i) => i.UserId == model.UserId));
                    if (u == null)
                    {
                        operationStatus.Status = false;
                        operationStatus.Message = "Sorry, but we are unble to assist in changing your password at this time. ";
                        return result;
                    }
                    else
                    {
                        var user = context.Users.First((Model.User me) => me.UserId == u.UserId);
                        user.Password = _Helper.Sha5121Encode(model.Password);
                        user.LastPasswordChangedDate = DateTime.Now;
                        if (context.SaveChanges() > 0)
                        {
                            operationStatus.Message = "User " + u.Firstname + " " + u.Lastname + " password reset successfully";
                            //SendEmailInBackgroundThread(u, model.Password, _settings.Appsettingshosturl, "PasswordReset", "IRAT AMS Password Reset");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_log.InsertOrUpdate(new Log { LoglevelId = (int)Enums.LogLevel.Error, Shortmessage = ex.Message, Fullmessage = OperationStatus.CreateException(ex), DateCreated = DateTime.Now });
            }
            return result;
        }

        public OperationStatus forgotpassword(LoginViewModel model)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            try
            {
                _audit.Insert(new Model.AuditTrail { AuditTypeId = (int)_Enums.AuditType.Security, Description = "Forgot password initiate for " + model.Email });
                if (string.IsNullOrEmpty(model.Email))
                {
                    operationStatus.Status = false;
                    operationStatus.Message = "The Email field is required.";
                    return result;
                }
                using (Context context = new Context())
                {
                    var u = Enumerable.FirstOrDefault(RetrieveAllUsers((User i) => i.Email == model.Email));
                    if (u == null)
                    {
                        operationStatus.Status = false;
                        operationStatus.Message = "We can't find a match for that email address. Check to be sure that you entered the email address you chose when you set up your account. Then try again.";
                        return result;
                    }
                    else
                    {
                        var user = context.Users.First((Model.User me) => me.UserId == u.UserId);
                        string passwd = _Helper.GeneratePassword(5);
                        user.Password = _Helper.Sha5121Encode(passwd);
                        if (context.SaveChanges() > 0)
                        {
                            operationStatus.Message = "A email was sent to " + u.Email + " with a password";
                            //_Audit.Insert(new Model.AuditTrail { AuditTypeId = (int)_Enums.AuditType.Security, Description = "Forgot password request successful for " + user.Firstname + " " + user.Lastname });
                            //SendEmailInBackgroundThread(u, passwd, "http://assetmgr.islandroutes.com", "ForgotPassword", "IRAT AMS Password Request");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_log.InsertOrUpdate(new Log { LoglevelId = (int)Enums.LogLevel.Error, Shortmessage = ex.Message, Fullmessage = OperationStatus.CreateException(ex), DateCreated = DateTime.Now });
            }
            return result;
        }

        public OperationStatus ChangePassword(ChangePassword model)
        {
            OperationStatus operationStatus = new OperationStatus();
            operationStatus.Status = true;
            OperationStatus result = operationStatus;
            try
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    operationStatus.Status = false;
                    operationStatus.Message = "Confirm password doesn't match";
                    return result;
                }
                using (Context context = new Context())
                {
                    var u = Enumerable.SingleOrDefault(RetrieveAllUsers((User i) => i.UserId == model.UserId));
                    if (u == null)
                    {
                        operationStatus.Status = false;
                        operationStatus.Message = "Sorry, but we are unble to assist in changing your password at this time. ";
                        _audit.Insert(new Model.AuditTrail { UserId = u.UserId, AuditTypeId = (int)_Enums.AuditType.Security, Description = "Password change attempt failed: " + model.Firstname + " " + model.Lastname });
                        return result;
                    }
                    else
                    {
                        var user = context.Users.First((Model.User me) => me.UserId == u.UserId);
                        user.Password = _Helper.Sha5121Encode(model.Password);
                        user.LastPasswordChangedDate = DateTime.Now;
                        if (context.SaveChanges() > 0)
                        {
                            operationStatus.Message = "User " + u.Firstname + " " + u.Lastname + " password reset successfully";
                            _audit.Insert(new Model.AuditTrail { UserId = u.UserId, AuditTypeId = (int)_Enums.AuditType.Security, Description = "Password change successfully: " + u.Username });
                            //SendEmailInBackgroundThread(u, model.Password, _settings.Appsettingshosturl, "ChangePassword", "IRAT AMS Password Change");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.InsertOrUpdate(new Log { LoglevelId = (int)_Enums.LogLevel.Error, Shortmessage = ex.Message, Fullmessage = OperationStatus.CreateException(ex), DateCreated = DateTime.Now });
            }
            return result;
        }

        public void SendEmailInBackgroundThread(User employee, string pwd, string portallink, string temple, string subject)
        {
            Thread bgThread = new Thread(delegate ()
            {
                //string body = _emailBuilderUser.buildMailBody(employee, pwd, portallink, temple);
                //_smtpemailLogic.Send(employee.Email, "", subject, body, "");
            });
            bgThread.IsBackground = true;
            bgThread.Start();
        }

        public static bool HasPermission(object parameter, Model.User user = null)
        {
            bool? permission = false;
            if (user != null)
            {
                using (Context context = new Context())
                {
                    string[] claim = parameter.ToString().Split('-');
                    AccessType accessType = (AccessType)Enum.Parse(typeof(AccessType), claim.First());

                    List<PermissionRf> permissionGroupRef = context.PermissionRfs.Include(i => i.PermissionGroup).Include(i => i.Permission).Where(i => i.PermissionGroupId == user.PermissionGroupId).ToList();
                    foreach (PermissionRf perm in permissionGroupRef)
                    {
                        //if (perm.Permission.AccessType == accessType)
                        //{
                        //    if (perm.Permission.Description == claim.Last())
                        //    {
                        //        permission = true;
                        //    }
                        //}
                    }
                }
                if (!permission.HasValue)
                {
                    return false;
                }
            }
            return permission.Value;
        }


    }
}
