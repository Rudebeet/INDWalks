using System;
namespace INDWalks.API.Models.DTO
{
	public class RegionDto
	{
        public Guid Id { get; set; }

        public String Code { get; set; }

        public String Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}

