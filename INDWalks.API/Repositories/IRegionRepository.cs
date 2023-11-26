﻿using System;
using INDWalks.API.Models.Domain;

namespace INDWalks.API.Repositories
{
	public interface IRegionRepository
	{
       Task<List<Region>> GetAllAsync();

       Task<Region?> GetByIDAsync(Guid id);

       Task<Region> CreateAsync(Region region);

        Task<Region> UpdateAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id);

    }
}
