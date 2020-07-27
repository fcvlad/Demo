using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class CompanyAddDto
    {    
        public string Name { get; set; }
        public string Introduction { get; set; }
        public ICollection<EmpolyeeAddDto> Employees { get; set; } = new List<EmpolyeeAddDto>();///防止空引用异常

    }
}
