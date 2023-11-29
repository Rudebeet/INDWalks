using System;
using Microsoft.AspNetCore.Identity;

namespace INDWalks.API.Repositories
{
	public interface ITokenRepository
	{
		string GenerateJWTToken(IdentityUser user, List<String> roles);
	}
}

