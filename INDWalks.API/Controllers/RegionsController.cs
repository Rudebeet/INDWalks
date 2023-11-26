using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using INDWalks.API.CustomActionFilter;
using INDWalks.API.Data;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    public class RegionsController : Controller
    {
        private readonly INDWalksDbContext _dbContext;
        private readonly IRegionRepository _regionrepository;
        private readonly IMapper _mapper;

        public RegionsController(INDWalksDbContext database, IRegionRepository regionRepository, IMapper mapper)
        {
            _dbContext = database;
            _regionrepository = regionRepository;
            _mapper = mapper;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Getting data from Databae
            List<Region> regionsDomain = await _regionrepository.GetAllAsync();

            //creating and assigning values to DTO
            /*var regionDto = new List<RegionDto>();

            foreach (Region RegionDomain in regionsDomain)
            {
                regionDto.Add(new RegionDto() { 
                    Id = RegionDomain.Id,
                    Code = RegionDomain.Code,
                    Name = RegionDomain.Name,
                    RegionImageURL = RegionDomain.RegionImageURL
                    });
            }*/

            //using AutoMapper
            var regionDto = _mapper.Map<List<RegionDto>>(regionsDomain);

            //return region DTO(Data Transfer Object)
            return Ok(regionDto);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            //retriving data from database
            Region? regionsDomain = await _regionrepository.GetByIDAsync(id);


            if(regionsDomain is null)
            {
                return NotFound();
            }
            //creatin and assign a value to DTO
            /*RegionDto regionDto = new RegionDto()
            {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageURL = regionsDomain.RegionImageURL
            };*/
            

            return Ok(_mapper.Map<RegionDto>(regionsDomain));

        }

        // POST api/values
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]RegionAddDto regionAddDto)
        {
            //Convert DTO to Domain Model
            /*Region regionDomain = new Region
            {
                Name = regionAddDto.Name,
                Code = regionAddDto.Code,
                RegionImageURL = regionAddDto.RegionImageURL
            };*/

            //Auto Mapper
            Region regionDomain = _mapper.Map<Region>(regionAddDto);

            //ADD Domain model to database
            regionDomain = await _regionrepository.CreateAsync(regionDomain);

            //map Doamin back to DTO
            /*RegionDto regionsDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageURL = regionDomain.RegionImageURL
            };*/

            //send response
            return CreatedAtAction(nameof(GetByID), new { id = _mapper.Map<RegionDto>(regionDomain).Id }, _mapper.Map<RegionDto>(regionDomain));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] RegionUpadateDto regionUpadateDto)
        {

            Region regionDomain = _mapper.Map<Region>(regionUpadateDto);

            regionDomain = await _regionrepository.UpdateAsync(id, regionDomain);

            if(regionDomain is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionDto>(regionDomain));

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Region? region = await _regionrepository.DeleteAsync(id);

            if(region is null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}

