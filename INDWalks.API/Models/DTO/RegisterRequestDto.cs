using System;
using System.ComponentModel.DataAnnotations;

namespace INDWalks.API.Models.DTO
{
	public class RegisterRequestDto
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string userName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string password { get; set; }

		[Required]
		public string[] Roles { get; set; }
	}
}

