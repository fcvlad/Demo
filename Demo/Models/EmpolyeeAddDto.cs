using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Entities;

namespace Demo.Models
{
    public class EmpolyeeAddDto
    {
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
