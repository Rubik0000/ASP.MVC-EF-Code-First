using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab1.Models
{
    public class AddAndEditPlayerModel
    {
        public AddAndEditPlayerModel()
        {
            SelctedTeamIds = new List<int>();
            AllTeams = new List<Team>();
        } 

        public AddAndEditPlayerModel(Player player) : this()
        {
            PlayerId = player.PlayerId;
            FirstName = player.FirstName;
            LastName = player.LastName;            
        }

        public int PlayerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }        

        public List<int> SelctedTeamIds { get; set; }

        public List<Team> AllTeams { get; set; }
    }
}