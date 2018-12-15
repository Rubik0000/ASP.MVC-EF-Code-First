using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public virtual List<Player> Players { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}