using System;
using System.ComponentModel.DataAnnotations;

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
        public virtual Providence Providence { get; set; }

        public DateTime? CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
