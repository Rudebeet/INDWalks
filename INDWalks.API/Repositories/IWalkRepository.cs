using System;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;

namespace INDWalks.API.Repositories
{
	public interface IWalkRepository
	{
		Task<Walk> CreateWalkAsync(Walk walkdomain);
		Task<List<Walk>> GetwalkAsync();
		Task<Walk?> GetWalkAsync(Guid id);
		Task<Walk?> UpdateWalkAsync(Guid id, Walk walkDomain);
        Task<Walk?> DeleteAsync(Guid id);
    }

}

