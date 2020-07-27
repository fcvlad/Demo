using Demo.Models;
using System.ComponentModel.DataAnnotations;

namespace Demo.ValidationAttributes
{
    public class EmployeeNoMustDifferentFromFirstNameAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addDto = (EmpolyeeAddOrUpdateDto)validationContext.ObjectInstance;
            if (addDto.EmployeeNo == addDto.FirstName)
            {
                return new ValidationResult(ErrorMessage,new[] { nameof(EmpolyeeAddOrUpdateDto)});
            }
            return ValidationResult.Success;
        }
    }
}
