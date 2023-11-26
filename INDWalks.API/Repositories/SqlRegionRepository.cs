using System;
using INDWalks.API.Data;
using INDWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace INDWalks.API.Repositories
{
	public class SqlRegionRepository : IRegionRepository
	{
        private readonly INDWalksDbContext _INDWalksDbContext;

        public SqlRegionRepository(INDWalksDbContext iNDWalksDbContext)
		{
            _INDWalksDbContext = iNDWalksDbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _INDWalksDbContext.regions.ToListAsync();
        }

        public async Task<Region?> GetByIDAsync(Guid id)
        {
           return await _INDWalksDbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _INDWalksDbContext.AddAsync(region);
            await _INDWalksDbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            Region? exisingRegion = await _INDWalksDbContext.regions.FirstOrDefaultAsync(x => x.Id == id);

            if(exisingRegion is null)
            {
                return exisingRegion;
            }

            exisingRegion.Name = region.Name;
            exisingRegion.Code = region.Code;
            exisingRegion.RegionImageURL = region.RegionImageURL;

            _INDWalksDbContext.Update(region);
            await _INDWalksDbContext.SaveChangesAsync();

            return exisingRegion;

        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            Region? exisingRegion = await _INDWalksDbContext.regions.FirstOrDefaultAsync(x => x.Id == id);

            if (exisingRegion is null)
            {
                return exisingRegion;
            }

            _INDWalksDbContext.regions.Remove(exisingRegion);
            await _INDWalksDbContext.SaveChangesAsync();

            return (exisingRegion);
        }
    }
}

