using System;
using INDWalks.API.Data;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INDWalks.API.Repositories
{
	public class SqlWalkRepository : IWalkRepository
	{
        private readonly INDWalksDbContext _dbcontext;

        public SqlWalkRepository(INDWalksDbContext indWalksDbContext)
		{
            _dbcontext = indWalksDbContext;
		}

        async Task<Walk> IWalkRepository.CreateWalkAsync(Walk walkDomain)
        {
            await _dbcontext.AddAsync(walkDomain);
            await _dbcontext.SaveChangesAsync();

            return (walkDomain);
        }


        async Task<List<Walk>> IWalkRepository.GetwalkAsync(string? filterOn, string? filterQuery, string? sortBy, bool isAscending,
            int pageNumber, int pageSize)
        {
            var walk = _dbcontext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = walk.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walk = isAscending ? walk.OrderBy(x=>x.Name): walk.OrderByDescending(x => x.Name);
                }
                if(sortBy.Equals("Length" , StringComparison.OrdinalIgnoreCase))
                {
                    walk = isAscending ? walk.OrderBy(x => x.LengthInKM) : walk.OrderByDescending(x => x.LengthInKM);
                }
            }

            //pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walk.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        async Task<Walk?> IWalkRepository.GetWalkAsync(Guid id)
        {
           return await _dbcontext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(X => X.ID == id);
        }

        async Task<Walk?> IWalkRepository.UpdateWalkAsync(Guid id, Walk walkDomain)
        {
            Walk? walk = await _dbcontext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(X => X.ID == id);

            if(walk is null)
            {
                return walk;
            }

            walk.Description = walkDomain.Description;
            walk.Name = walkDomain.Name;
            walk.LengthInKM = walkDomain.LengthInKM;
            walk.WalkImageURL = walkDomain.WalkImageURL;
            walk.DifficultyID = walkDomain.DifficultyID;
            walk.RegionID = walkDomain.RegionID;

            _dbcontext.Walks.Update(walk);
            await _dbcontext.SaveChangesAsync();

            return walkDomain;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            Walk? walk = await _dbcontext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(X => X.ID == id);

            if (walk is null)
            {
                return walk;
            }

            _dbcontext.Remove(walk);
            await _dbcontext.SaveChangesAsync();

            return walk;
        }

    }
}

