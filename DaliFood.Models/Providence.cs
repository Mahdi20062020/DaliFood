using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DaliFood.Models
{
    public class Providence
    {
        // this.propertis
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PreNumber { get; set; }
        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }

        // Navigation Properties
        public virtual IEnumerable<City> Cities { get; set; }
    }
}
