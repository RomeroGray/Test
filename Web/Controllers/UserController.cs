using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IRAT.Inject.Model;
using IRAT.Inject.Model.Shared;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IRAT.Inject.Business._User _user;
        private readonly IRAT.Inject.Business._Location _location;
        private readonly IRAT.Inject.Business._Permission _permission;

        public UserController()
        {
            this._user = new IRAT.Inject.Business._User();
            this._location = new IRAT.Inject.Business._Location();
            this._permission = new IRAT.Inject.Business._Permission();

            ViewBag.Location = Enumerable.ToList(_location.RetrieveAllLocations((Location i) => i.Deleted == false));
            ViewBag.PermissionGroups = Enumerable.ToList(_permission.RetrieveAllPermissionGroups(null));
        }

        public ActionResult Index()
        {
            return View(Enumerable.ToList(_user.RetrieveAllUsers(null, (User i) => i.Location, (User i) => i.PermissionGroup)));
        }

        public ActionResult settings()
        {
            return View(Enumerable.FirstOrDefault(_user.RetrieveAllUsers(null, (User i) => i.Location, (User i) => i.PermissionGroup)));
        }

        public ActionResult Search(string keyword, int? locationId, int? permissiongroupId, bool? status, string daterange, bool? chkbxdate)
        {
            IEnumerable<User> list = null; //_user.Search(keyword, departmentId, permissiongroupId, status, daterange, chkbxdate);
            if (list.Count() == 0)
            {
                return View("index", new List<User>());
            }
            return View("index", list);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = Enumerable.FirstOrDefault(_user.RetrieveAllUsers((User i) => i.Gid == id, (User i) => i.PermissionGroup, (User i) => i.Location));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = Enumerable.FirstOrDefault(_user.RetrieveAllUsers((User i) => i.Gid == id, (User i) => i.PermissionGroup, (User i) => i.Location));
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        } 

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(User user)
        {
            JsonResult result = null;
            OperationStatus operationStatus = _user.InsertOrUpdate(user);
            if (operationStatus.Status)
            {
                return result = base.Json(new
                {
                    ok = true,
                    Id = operationStatus.Id,
                    message = operationStatus.Message,
                }, JsonRequestBehavior.AllowGet); ;
            }
            else
            {
                return result = base.Json(new
                {
                    ok = false,
                    message = operationStatus.Message,
                }, JsonRequestBehavior.AllowGet);
            }
        }


        [ActionName("password-reset")]
        public ActionResult passwordreset(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(Enumerable.FirstOrDefault(_user.RetrieveAllUsers((User u) => u.Gid == id)));
        }

        [HttpPost]
        public ActionResult ResetPassword(User user)
        {
            JsonResult result = null;
            OperationStatus operationStatus = _user.ResetPassword(user);
            if (operationStatus.Status)
            {
                return result = base.Json(new
                {
                    ok = true,
                    Id = operationStatus.Id,
                    message = operationStatus.Message,
                }, JsonRequestBehavior.AllowGet); ;
            }
            else
            {
                return result = base.Json(new
                {
                    ok = false,
                    message = operationStatus.Message,
                }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}
