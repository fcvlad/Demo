using AutoMapper;
using Demo.Entities;
using Demo.Models;
using System;


namespace Demo.Profiles
{
    public class EmployeeProfile:Profile
    {/// <summary>
    /// 源，目标
    /// </summary>
        public EmployeeProfile()
        {
            CreateMap<Employee,EmployeeDto>()
                .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>$"{src.FirstName} {src.LastName}"))
                .ForMember(dest=>dest.GenderDisplay,opt=>opt.MapFrom(src=>src.Gender.ToString()))
                .ForMember(dest=>dest.Age,opt=>opt.MapFrom(src=>DateTime.Now.Year-src.DateOfBirth.Year))
                ;
            CreateMap<EmpolyeeAddOrUpdateDto,Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeUpdateDto>();
        }
    }
}
