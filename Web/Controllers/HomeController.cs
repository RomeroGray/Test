using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IRAT.Inject.Model;
using IRAT.Inject.Model.Class;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRAT.Inject.Business._Source _source;
        public readonly IRAT.Inject.Business._ResmarkEndpoints _resmarkEndpoints;

        public HomeController()
        {
            this._source = new IRAT.Inject.Business._Source();
            this._resmarkEndpoints = new IRAT.Inject.Business._ResmarkEndpoints();
        }

        public async Task<ActionResult> Index()
        {
            //List<DataLookup> json = _resmarkEndpoints.ReadJsonfile();
            //ViewBag.Lookup = _resmarkEndpoints.ReadJsonfile();
            //string GetTransaction = await _resmarkEndpoints.TransactionDateDetails("2022-10-28", "2022-10-30", "", "", "");
            //Tranactions tranactions = JsonConvert.DeserializeObject<Tranactions>(GetTransaction);
            return View();
        }

        public async Task<ActionResult> Search(string daterange, bool? chkbxdate)
        {
            //ViewBag.Lookup = _resmarkEndpoints.ReadJsonfile();
            ViewBag.Lookup =  Enumerable.ToList(_source.RetrieveAllSources(null, (MatchTable i) => i.Tier, (MatchTable i) => i.User, (MatchTable i) => i.Country));
            string GetTransaction = await _resmarkEndpoints.TransactionDateDetails(daterange, "", "", "");
            Tranactions tranactions = JsonConvert.DeserializeObject<Tranactions>(GetTransaction);
            if (tranactions == null)
            {
                return View(tranactions);
            }
            return View("index", tranactions);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}