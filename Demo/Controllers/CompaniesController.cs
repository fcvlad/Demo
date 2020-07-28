using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Demo.DtoParameters;
using Demo.Entities;
using Demo.Helps;
using Demo.Models;
using Demo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Demo.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompaniesController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(companyRepository));
        }
        [HttpGet(Name =(nameof(GetCompanies)))]
        [HttpHead]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyDtoParameters parameters)//指定绑定来源
        {
            var companies = await _companyRepository.GetCompaniesAsync(parameters);
            var previousPageLink = companies.HasPrevious ? CreateCompaniesResoureUri(parameters, ResourceUriType.PreviousPage) : null;
            var nextPageLink = companies.HasNext ? CreateCompaniesResoureUri(parameters, ResourceUriType.NextPage) : null;
            var pageinationMetadata = new
            {
                totalCount=companies.TotalCount,
                pageSize=companies.PageSize,
                currentPage=companies.CurrentPage,
                totalPages=companies.TotalPages,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pageinationMetadata, 
                new JsonSerializerOptions{ Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));

            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companyDtos);
        }
        [HttpGet("{companyId}", Name = nameof(GetCompany))]
        public async Task<IActionResult> GetCompany(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyAddDto company)
        {
            var entity = _mapper.Map<Company>(company);
            _companyRepository.AddCompany(entity);
            await _companyRepository.SaveAsync();

            var returnDto = _mapper.Map<CompanyDto>(entity);
            return CreatedAtRoute(nameof(GetCompany), new { companyId = returnDto.Id }, returnDto);
        }
        [HttpPost]
        [Route("api/companycollections")]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection(IEnumerable<CompanyAddDto> companyCollection)
        {
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var company in companyEntities)
            {
                _companyRepository.AddCompany(company);
            }
            await _companyRepository.SaveAsync();
            var returnDtos = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var idsString = string.Join(",", returnDtos.Select(x => x.Id));
            return CreatedAtRoute(nameof(GetCompanyCollection),new { ids=idsString} , returnDtos);
        }
        [HttpGet("({ids})",Name =(nameof(GetCompanyCollection)))]
        public async Task<IActionResult> GetCompanyCollection([FromRoute] [ModelBinder(BinderType =typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }
            var entities = await _companyRepository.GetCompaniesAsync(ids);
            if (ids.Count() != entities.Count())
            {
                return NotFound();
            }
            var RetrunDto = _mapper.Map<IEnumerable<CompanyDto>>(entities);
            return Ok(RetrunDto);
        }
        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            var companyEntity = await _companyRepository.GetCompanyAsync(companyId);
            if (companyEntity == null)
            {
                return NotFound();
            }
            _companyRepository.DeleteCompany(companyEntity);
            await _companyRepository.SaveAsync();

            return NoContent();
        }

        [HttpOptions]
        public  IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow","GET,POST,OPTIONS");
            return Ok();
        }
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string CreateCompaniesResoureUri(CompanyDtoParameters parameter, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetCompanies),new
                    {
                        pageNumber = parameter.PageNumber - 1,
                        pageSzie = parameter.PageSize,
                        companyName = parameter.CompanyName,
                        searchTerm = parameter.SearchTerm
                    });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        pageNumber = parameter.PageNumber + 1,
                        pageSzie = parameter.PageSize,
                        companyName = parameter.CompanyName,
                        searchTerm = parameter.SearchTerm
                    });
                default:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        pageNumber = parameter.PageNumber,
                        pageSzie = parameter.PageSize,
                        companyName = parameter.CompanyName,
                        searchTerm = parameter.SearchTerm
                    });
            }
        }

    }
}
