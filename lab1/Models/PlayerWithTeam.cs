using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab1.Models
{
    public class PlayerWithTeam
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TeamName { get; set; }
    }
}