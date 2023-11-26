using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using INDWalks.API.CustomActionFilter;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INDWalks.API.Controllers
{

    [Route("api/[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;

        public WalksController(IWalkRepository walksRepository, IMapper mapper)
        {
            _walkRepository = walksRepository;
            _mapper = mapper;
        }

        //Create a Walk
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody]WalkAddDto walkAddDto)
        {
            //map Dto to domain model
            Walk walkDomain = _mapper.Map<Walk>(walkAddDto);

            //Insert data using repository
            await _walkRepository.CreateWalkAsync(walkDomain);

            return Ok(_mapper.Map<Walk>(walkAddDto));
        }

        //Get all Walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            List<Walk> walk = await _walkRepository.GetwalkAsync();

            return Ok(_mapper.Map<List<WalkDto>>(walk));
        }

        // GET walks by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            Walk? walkDomain = await _walkRepository.GetWalkAsync(id);

            if(walkDomain is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkDomain));

        }

        //Update walk by Id
        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateById ([FromRoute]Guid id,[FromBody] WalkUpdatedto walkUpdatedto)
        {
            Walk? walkDomain = _mapper.Map<Walk>(walkUpdatedto);

            walkDomain = await _walkRepository.UpdateWalkAsync(id, walkDomain);

            if(walkDomain is null)
            {
                return NotFound();
            }

            return Ok (_mapper.Map<WalkDto>(walkDomain));
        }

        //Delete by Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletebyId (Guid id)
        {
            Walk? walkDomain = await _walkRepository.DeleteAsync(id);

            if(walkDomain is null)
            {
                return NotFound();
            }

            return Ok(walkDomain);

        }
    }
}

