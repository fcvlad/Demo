using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    public class CompanyAddDto
    {
        [Display(Name = "公司名称")]
        [Required(ErrorMessage = "{0}名称不能未空")]
        [MaxLength(100,ErrorMessage ="{0}的最大长度不能超过{1}")]
        public string Name { get; set; }
        [Display(Name = "公司简介")]
        [StringLength(500,MinimumLength =10,ErrorMessage ="{0}的长度范围从{2}到{1}")]
        public string Introduction { get; set; }
        public ICollection<EmpolyeeAddOrUpdateDto> Employees { get; set; } = new List<EmpolyeeAddOrUpdateDto>();///防止空引用异常

    }
}
