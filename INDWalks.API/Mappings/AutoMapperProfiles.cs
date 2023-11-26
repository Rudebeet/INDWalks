using System;
using AutoMapper;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;

namespace INDWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<RegionDto, Region>().ReverseMap();
			CreateMap<RegionAddDto, Region>().ReverseMap();
			CreateMap<RegionUpadateDto, Region>().ReverseMap();

			CreateMap<WalkAddDto, Walk>().ReverseMap();
			CreateMap<Walk, WalkDto>().ReverseMap();

			CreateMap<DifficultyDto, Difficulty>().ReverseMap();

			CreateMap<WalkUpdatedto, Walk>().ReverseMap();
		}
	}
}

