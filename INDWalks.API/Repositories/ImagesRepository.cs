using System;
using INDWalks.API.Data;
using INDWalks.API.Models.Domain;

namespace INDWalks.API.Repositories
{
	public class ImagesRepository : IImagesRepository
	{
        private IWebHostEnvironment _webHostEnvironment;
        private IHttpContextAccessor _httpContextAccessor;
        private INDWalksDbContext _dbContext;

        public ImagesRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, INDWalksDbContext dbContext)
		{
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
		}

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            //upload Image to Local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //https://localhost:123/Images/Images.png

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}" +
                $"{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            //Add Image data to database
            await _dbContext.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;

        }
    }
}
 
