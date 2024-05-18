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
		public JsonResult GetCollections()
		{
			var res = _apibase.PCallApi<List<Collection>>(string.Format(ConstantHelper.GetCollections), string.Empty, ConfigKeys.CMSAPIBaseUrl, Method.GET, "application/json", ParameterType.RequestBody, "application/json");

			return Json(res.Result, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Edit(string id)
		{
			var model = new Collection();
			var res = _apibase.CallApi<Collection>(string.Format(ConstantHelper.GetCollection,id), string.Empty, ConfigKeys.CMSAPIBaseUrl, Method.GET, "application/json", ParameterType.RequestBody, "application/json");
			if (res != null && res.Status == "Success" && res.Result != null)
			{
				model=res.Result;
			}
			return View(model);
		}

		[HttpPost]
		public JsonResult UpdateCollection(CollectionModel updatedCollection)
		{

			try
			{
				var endpoint = string.Format(ConstantHelper.updateCollection, updatedCollection.Id); // Assuming `updatedCollection` has an Id property
				var res = _apibase.CallApi<Collection>(endpoint, JsonConvert.SerializeObject(updatedCollection), ConfigKeys.CMSAPIBaseUrl, Method.PUT, "application/json", ParameterType.RequestBody, "application/json");

				if (res != null && res.Status == "Success" && res.Result != null)
				{
					return Json(res, JsonRequestBehavior.AllowGet);
				}
				else
				{
					return Json(new { success = false, message = "Failed to update collection." });
				}
			}
			catch (Exception ex)
			{
				// Log the exception
				// Logger.LogError("Error in EditPage", ex);
				return Json(new { success = false, message = ex.Message });
			}

		}
		//public JsonResult GetDetail( string id)
		//{
		//	var res = _apibase.PCallApi<List<Collection>>(string.Format(ConstantHelper.GetCollections), string.Empty, ConfigKeys.CMSAPIBaseUrl, Method.GET, "application/json", ParameterType.RequestBody, "application/json");

		//	return Json(res.Result, JsonRequestBehavior.AllowGet);
		//}

		
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

		[HttpGet]
		public ActionResult Delete(string id)
		{
			var model = new Collection();
			var res = _apibase.CallApi<Collection>(string.Format(ConstantHelper.DeleteCollection, id), string.Empty, ConfigKeys.CMSAPIBaseUrl, Method.DELETE, "application/json", ParameterType.RequestBody, "application/json");
			if (res != null && res.Status == "Success")
			{
				model = res.Result;//not to use 
			}
			return RedirectToAction("Index");
		}


		//[HttpGet]
		//public JsonResult Delete(string id)
		//{
		//	var model = new Collection();
		//	var res = _apibase.CallApi<Collection>(string.Format(ConstantHelper.DeleteCollection, id), string.Empty, ConfigKeys.CMSAPIBaseUrl, Method.DELETE, "application/json", ParameterType.RequestBody, "application/json");
		//	if (res != null && res.Status == "Success" && res.Result != null)
		//	{
		//		model = res.Result;
		//	}
		//	return Json(res, JsonRequestBehavior.AllowGet);
		//}

	}
}