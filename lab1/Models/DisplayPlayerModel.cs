using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab1.Models
{
    public class DisplayPlayerModel
    {
        public Player Player { get; set; }        
        public IEnumerable<string> TeamNames { get; set; }
    }
}