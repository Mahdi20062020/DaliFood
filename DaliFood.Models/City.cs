using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaliFood.Models
{
    public class City
    {
        // this.properties
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        // navigation properties
        
        public int ProvidenceId { get; set; }

        [ForeignKey(nameof(ProvidenceId))]
        public virtual Providence Providence { get; set; }
        public virtual IEnumerable<Customer> Customers { get; set; }

        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
