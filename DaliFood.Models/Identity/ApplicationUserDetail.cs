using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaliFood.Models.Identity
{
    public class ApplicationUserDetail
    {
        [Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
