using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Demo.Models
{
    public class ApplicationUser:IdentityUser
    {
        [MaxLength(18)]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get;  set; }
    }
}
