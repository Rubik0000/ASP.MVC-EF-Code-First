using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab1.Models
{
    public class ShowPlayerModel
    {
        public Player Player { get; set; }
        public string Selected { get; set; }
        public IEnumerable<SelectListItem> Teams { get; set; }
    }
}