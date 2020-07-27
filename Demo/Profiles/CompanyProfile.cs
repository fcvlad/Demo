using AutoMapper;
using Demo.Entities;
using Demo.Models;

namespace Demo.Profiles
{
    public class CompanyProfile:Profile
    {
        /// <summary>
        /// 源,目标
        /// </summary>
        public CompanyProfile()
        {
            CreateMap<Company,CompanyDto>().ForMember
                (
                  dest=>dest.CompanyName,
                  opt=>opt.MapFrom(src=>src.Name)
                );
            CreateMap<CompanyAddDto,Company>();
        }
    }
}
