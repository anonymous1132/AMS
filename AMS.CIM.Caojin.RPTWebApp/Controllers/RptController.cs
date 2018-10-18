using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.CIM.Caojin.RPTWebApp.Models;
using System.Threading;
using System.Web.Services;

namespace AMS.CIM.Caojin.RPTWebApp.Controllers
{
    public class RptController : Controller
    {
        // GET: Rpt
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReqRpt018()
        {
            //ShareDataEntity.GetSingleEntity().Rpt018.GetData();
            ViewBag.Modules = ShareDataEntity.GetSingleEntity().Rpt018.Modules;
            ViewBag.EqpTypes = ShareDataEntity.GetSingleEntity().Rpt018.EqpType;
            ViewBag.EqpID = ShareDataEntity.GetSingleEntity().Rpt018.EqpID;
            ViewBag.QueryContent = ShareDataEntity.GetSingleEntity().Rpt018.SelectedContent;
            return View();
        }

        [HttpPost]
        public string Query(ReqRpt018PostViewModel viewModel)
        {
          
            string msg = "{\"success\":true,\"message\":\"" + viewModel.eqptype + " " + viewModel.from + "~" + viewModel.to+ "\"}";

            return msg;

        }
    }


}