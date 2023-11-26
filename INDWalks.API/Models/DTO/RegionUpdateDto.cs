using System;
using System.ComponentModel.DataAnnotations;

namespace INDWalks.API.Models.DTO
{
	public class RegionUpadateDto
	{
        [Required]
        [MinLength(3, ErrorMessage = "The length of code should be 3 characters.")]
        [MaxLength(3, ErrorMessage = "The length of code should be 3 characters.")]
        public String Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name should not be more than 100 characters long")]
        public String Name { get; set; }

        public string? RegionImageURL { get; set; }
    }
}

