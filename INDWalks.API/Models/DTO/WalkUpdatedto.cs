using System;
using System.ComponentModel.DataAnnotations;

namespace INDWalks.API.Models.DTO
{
	public class WalkUpdatedto
	{
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }

        [Required]
        [MaxLength(500)]
        public String Description { get; set; }

        [Required]
        [Range(3, 50)]
        public double LengthInKM { get; set; }

        public String? WalkImageURL { get; set; }

        [Required]
        public Guid DifficultyID { get; set; }

        [Required]
        public Guid RegionID { get; set; }
    }
}

