using CMSFrontend.Helpers;
using CMSFrontend.Models;
using CMSFrontend.Service;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace CMSFrontend.Controllers
{
    public class CmsController : Controller
    {

        ApiBase _apibase = new ApiBase();
        // GET: Cms
        public ActionResult Index()
        {
            return View();
        }
		public JsonResult ListPage1()
		{
			var res = _apibase.PCallApi<List<Collection>>(string.Format(ConstantHelper.GetCollections), string.Empty, ConfigKeys.CMSAPIBaseUrl, Method.GET, "application/json", ParameterType.RequestBody, "application/json");

			return Json(res.Result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult CreateCollection()
        {
            return View();
        }
	
		public JsonResult SaveCollection(CollectionModel collection)
		{
			var res = _apibase.CallApi<CollectionModel>(string.Format(ConstantHelper.CreateCollection), JsonConvert.SerializeObject(collection), ConfigKeys.CMSAPIBaseUrl, Method.POST, "application/json", ParameterType.RequestBody, "application/json");
			if (res != null && res.Status == "Success" && res.Result != null)
			{
				var a = res.Result;
				return Json(new { success = true, data = a, message = res.Message });
			}
			else
			{
				return Json(new { success = false, message = "Failed to save collection." });
			}
		}

	}
}