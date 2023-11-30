using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private IImagesRepository _imageRepository;

        public ImagesController(IImagesRepository imagesRepository)
        {
            _imageRepository = imagesRepository;
        }

        //Post api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload(ImageUploadRequestDto requestDto)
        {
            Validatefile(requestDto);

            if(ModelState.IsValid)
            {
                //Convert Dto to Domain
                var imageDomainModel = new Image
                {
                    File = requestDto.File,
                    FileExtension = Path.GetExtension(requestDto.File.FileName),
                    FileSizeInBytes = requestDto.File.Length,
                    FileName = requestDto.FileName,
                    FileDescription = requestDto.FileDescription
                };

                //Repository to upload file
                await _imageRepository.Upload(imageDomainModel);

                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void Validatefile(ImageUploadRequestDto requestDto)
        {
            var allowedExtendions = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtendions.Contains(Path.GetExtension(requestDto.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid File Extension");
            }

            if(requestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "Maximum file size");
            }
        }
    }
}

