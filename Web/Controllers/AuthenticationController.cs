using IRAT.Inject.Business;
using IRAT.Inject.Model;
using IRAT.Inject.Model.Shared;
using IRAT.Inject.UIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static IRAT.Inject.Business._Enums;

namespace Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRAT.Inject.Business._Log _log;
        private readonly IRAT.Inject.Business._User _user;
        private readonly IRAT.Inject.Business._Location _location;
        private readonly IRAT.Inject.Business._Permission _permission;

        public AuthenticationController()
        {
            this._log = new IRAT.Inject.Business._Log();
            this._user = new IRAT.Inject.Business._User();
            this._location = new IRAT.Inject.Business._Location();
            this._permission = new IRAT.Inject.Business._Permission();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            HttpCookie _cookie = Request.Cookies["IRATUSER"];
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            if (_cookie != null)
            {
                LoginViewModel login = new LoginViewModel()
                {
                    RememberMe = Convert.ToBoolean(_cookie["RememberMe"]),
                    Username = _cookie["Username"].ToString()
                };
                return View(login);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public JsonResult Login(LoginViewModel model)
        {
            JsonResult result = null;
            string message = "";
            string returnUrl = "/";
            try
            {
                var loginResult = _user.ValidateUser(model.Username, model.Password);
                switch (loginResult)
                {
                    case UserLoginResults.Successful:
                        {
                            bool persistanceFlag = model.RememberMe;
                            IRAT.Inject.Model.User user = _user.GetUser(model.Username);
                            string roles = user.PermissionGroup.Description;

                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.UserId.ToString(), DateTime.Now, DateTime.Now.AddMinutes(60.0), persistanceFlag, roles, FormsAuthentication.FormsCookiePath);
                            string encTicket = FormsAuthentication.Encrypt(ticket);
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                            Response.Cookies.Add(cookie);
                            if (persistanceFlag)
                            {
                                HttpCookie _cookie = new HttpCookie("IRATUSER");
                                _cookie["RememberMe"] = model.RememberMe.ToString();
                                _cookie["Username"] = model.Username;
                                _cookie.Expires = DateTime.Now.AddDays(90);
                                Response.Cookies.Add(_cookie);
                            }
                            else
                            {
                                HttpCookie _cookie = Request.Cookies["IRATUSER"];
                                if (_cookie != null)
                                {
                                    _cookie.Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies.Add(_cookie);
                                }
                            }
                            if (base.Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                            {
                                //return Redirect(returnUrl);
                                return result = base.Json(new
                                {
                                    ok = true,
                                    message = returnUrl,
                                }, JsonRequestBehavior.AllowGet);
                            }
                            return result = base.Json(new { ok = true, message = returnUrl, }, JsonRequestBehavior.AllowGet);
                        }
                    case UserLoginResults.UserNotExist:
                        message = ErrorCodeToString(UserLoginResults.UserNotExist);
                        break;
                    case UserLoginResults.Deleted:
                        message = ErrorCodeToString(UserLoginResults.Deleted);
                        break;
                    case UserLoginResults.NotActive:
                        message = ErrorCodeToString(UserLoginResults.NotActive);
                        break;
                    case UserLoginResults.LockedOut:
                        message = ErrorCodeToString(UserLoginResults.LockedOut);
                        break;
                    case UserLoginResults.WrongPassword:
                    default:
                        message = ErrorCodeToString(UserLoginResults.WrongPassword);
                        //ModelState.AddModelError("", ErrorCodeToString(UserLoginResults.WrongPassword));
                        break;
                }

            }
            catch (Exception ex)
            {
                _log.InsertOrUpdate(new Log { LoglevelId = (int)_Enums.LogLevel.Error, Shortmessage = ex.Message, Fullmessage = OperationStatus.CreateException(ex), DateCreated = DateTime.Now });
            }
            return result = base.Json(new { ok = false, message = message }, JsonRequestBehavior.AllowGet);
        }

        private string ErrorCodeToString(UserLoginResults LoginStatus)
        {
            switch (LoginStatus)
            {
                case UserLoginResults.Successful:
                    return "The username or password provided is correct.";
                case UserLoginResults.UserNotExist:
                    return "Sorry, your username and password that you've entered doesn't match any account.";
                case UserLoginResults.WrongPassword:
                    return "The password provided is invalid. Please enter a valid password.";
                case UserLoginResults.NotActive:
                    return "Sorry, your account is inactive. Please contact the administrator.";
                case UserLoginResults.Deleted:
                    return "You account has been deleted due to Terms of Service voilations.";
                case UserLoginResults.NotRegistered:
                    return "";
                case UserLoginResults.LockedOut:
                    return "Your account has been locked out because of too many invalid login attempts. Please contact the administrator.";
                case UserLoginResults.ExceedLoginAttempts:
                    return "Your Password is incorrect. You have one attempt remaining. Please contact the administrator.";
                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            if (User.Identity.IsAuthenticated)
            {
                OperationStatus operationStatus = _user.LogOff();
                if (operationStatus.Status)
                {
                    FormsAuthentication.SignOut();
                }
                return RedirectToAction("index", "home");
            }
            return RedirectToAction("Login", "Authentication");
        }

    }
}