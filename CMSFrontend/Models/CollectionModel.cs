﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSFrontend.Models
{
    public class CollectionModel
    {
		public string Id { get; set; }
		public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
    }
}