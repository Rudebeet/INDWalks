using System;
namespace INDWalks.API.Models.DTO
{
	public class WalkDto
	{
        public String Name { get; set; }

        public String Description { get; set; }

        public double LengthInKM { get; set; }

        public String? WalkImageURL { get; set; }


        public RegionDto Region { get; set; }

        public DifficultyDto Difficulty { get; set; }
    }
}

