using CMSFrontend.Helpers;
using CMSFrontend.Models;
using CMSFrontend.Service;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult CreateCollection()
        {
            return View();
        }  
        
        public ActionResult saveCollection(CollectionModel collection)
        {
            var res = _apibase.CallApi<CollectionModel>(string.Format(ConstantHelper.CreateCollection), JsonConvert.SerializeObject(collection), ConfigKeys.CMSAPIBaseUrl, Method.POST, "application/json", ParameterType.RequestBody, "application/json");
            if (res != null && res.Result != null)
            {
                var a = res.Result;
            }
            return View();
        }
    }
}