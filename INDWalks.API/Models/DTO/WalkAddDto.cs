using System;
namespace INDWalks.API.Models.DTO
{
	public class WalkAddDto
	{
        public String Name { get; set; }

        public String Description { get; set; }

        public double LengthInKM { get; set; }
    
        public String? WalkImageURL { get; set; }

        public Guid DifficultyID { get; set; }

        public Guid RegionID { get; set; }
    }
}

