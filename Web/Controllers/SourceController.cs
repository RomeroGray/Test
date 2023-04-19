using IRAT.Inject.Model;
using IRAT.Inject.Model.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SourceController : Controller
    {
        private readonly IRAT.Inject.Business._Log _log;
        private readonly IRAT.Inject.Business._Tier _tier;
        private readonly IRAT.Inject.Business._Source _source;
        private readonly IRAT.Inject.Business._Country _country;

        public SourceController()
        {
            this._log = new IRAT.Inject.Business._Log();
            this._tier = new IRAT.Inject.Business._Tier();
            this._source = new IRAT.Inject.Business._Source();
            this._country = new IRAT.Inject.Business._Country();

            ViewBag.Tier = Enumerable.ToList(_tier.RetrieveAllTiers(null));
            ViewBag.Country = Enumerable.ToList(_country.RetrieveAllCountries((Country i) => i.Published == true));
        }

        public ActionResult Search(string keyword, string supplier, string daterange, bool? chkbxdate)
        {
            IEnumerable<MatchTable> list = _source.Search(keyword, supplier, daterange, chkbxdate);
            if (list.Count() == 0)
            {
                return View("index", new List<MatchTable>());
            }
            return View("index", list);
        }

        public JsonResult GetSourceByProductNo(int id)
        {
            IEnumerable<MatchTable> list = Enumerable.ToList(_source.RetrieveAllSources(i => i.ProductNumber == id, (MatchTable i) => i.Tier, (MatchTable i) => i.User, (MatchTable i) => i.Country));
            if (list.Count() > 0)
            {
                var result = list.Select(i => new
                {
                    i.Gid,
                    i.MatchTableId,
                    i.ProductNumber,
                    i.Cost,
                    i.Name,
                    i.PickupZone,
                    i.Supplier,
                    TierId = i.Tier.Description,
                    CountryId = i.Country.Description
                });
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View(Enumerable.ToList(_source.RetrieveAllSources(null, (MatchTable i) => i.Tier, (MatchTable i) => i.User, (MatchTable i) => i.Country)).Take(20));
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchTable data = Enumerable.FirstOrDefault(_source.RetrieveAllSources(null, (MatchTable i) => i.Tier, (MatchTable i) => i.User, (MatchTable i) => i.Country));
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchTable data = Enumerable.FirstOrDefault(_source.RetrieveAllSources(null, (MatchTable i) => i.Tier, (MatchTable i) => i.User, (MatchTable i) => i.Country));
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public JsonResult Save(MatchTable matchTable)
        {
            JsonResult result = null;
            OperationStatus operationStatus = _source.InsertOrUpdate(matchTable);
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
                    errormgs = operationStatus.ExceptionMessage,
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}