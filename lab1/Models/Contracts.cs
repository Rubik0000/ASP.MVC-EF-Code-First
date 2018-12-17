using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lab1.Models
{
    /// <summary>
    /// The contracts table realizes the many-to-many realation
    /// </summary>
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractId { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }

        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}