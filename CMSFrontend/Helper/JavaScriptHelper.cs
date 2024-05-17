using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace CMSFrontend.Helper
{
	public static class JavaScriptHelper
	{
		public static IHtmlString Json(this HtmlHelper helper, object obj)
		{
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				Converters = new JsonConverter[]
				{
					new StringEnumConverter(),
				},
				StringEscapeHandling = StringEscapeHandling.EscapeHtml
			};

			return MvcHtmlString.Create(JsonConvert.SerializeObject(obj, settings));
		}
		// Method to convert a model to JSON string with camelCase property names
		public static string ConvertToJson(object model)
		{
			// Configure JsonSerializerSettings to use camelCase for property names
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			// Serialize the model object to JSON string with camelCase property names
			string json = JsonConvert.SerializeObject(model, settings);

			return json;
		}

	}
	
}