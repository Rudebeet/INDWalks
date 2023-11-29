using System;
using System.ComponentModel.DataAnnotations;

namespace INDWalks.API.Models.DTO
{
	public class LoginRequestDto
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string userName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public String password { get; set; }
	}
}

