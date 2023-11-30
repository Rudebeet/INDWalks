using System;
using INDWalks.API.Models.Domain;

namespace INDWalks.API.Repositories
{
	public interface IImagesRepository
	{
        Task<Image> Upload(Image image);
    }
}

