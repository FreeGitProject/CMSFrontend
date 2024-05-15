using System;
using System.Collections.Generic;
using System;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CMSFrontend.Models
{
	
	public class Collection
	{
		
		public string Id { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public string Image { get; set; }
		public string Link { get; set; }
	}
}