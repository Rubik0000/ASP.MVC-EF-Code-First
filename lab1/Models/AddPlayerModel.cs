using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab1.Models
{
    public class AddPlayerModel
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public Team Team { get; set; }
        //[NotMapped]
        public List<string> SelctedTeams { get; set; }
        public List<SelectListItem> AllTeams { get; set; }
    }
}