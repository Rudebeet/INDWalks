using System;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace INDWalks.API.Repositories
{
	public interface IWalkRepository
	{
		Task<Walk> CreateWalkAsync(Walk walkdomain);
		Task<List<Walk>> GetwalkAsync(string? filterOn =null, string? filterQuery=null,string? sortBy=null, bool isAscending=true,
			int pageNumber=1, int pageSize=1000);
		Task<Walk?> GetWalkAsync(Guid id);
		Task<Walk?> UpdateWalkAsync(Guid id, Walk walkDomain);
        Task<Walk?> DeleteAsync(Guid id);
    }

}

