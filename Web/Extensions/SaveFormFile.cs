using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extensions
{
    /// <summary>
    /// Save form file extention service
    /// </summary>
    public class SaveFormFileSerivce
    {
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construct services
        /// </summary>
        /// <param name="webHostingEnvironment"></param>
        /// <param name="configuration"></param>
        public SaveFormFileSerivce(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration)
        {
            _webHostingEnvironment = webHostingEnvironment;
            _configuration = configuration;
        }

        /// <summary>
        /// Save category images
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> SaveCategoryImage(IFormFile file)
        {
            string pathForSaveImage = _configuration.GetValue<string>("Paths:CategoryImagesUploadPath");
            return await SaveAs(file, pathForSaveImage);
        }

        /// <summary>
        /// Save brand images
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> SaveBrandImage(IFormFile file)
        {
            string pathForSaveImage = _configuration.GetValue<string>("Paths:BrandImagesUploadPath");
            return await SaveAs(file, pathForSaveImage);
        }

        /// <summary>
        /// Save product images
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> SaveProductImage(IFormFile file)
        {
            string pathForSaveImage = _configuration.GetValue<string>("Paths:ProductImagesUploadPath");
            return await SaveAs(file, pathForSaveImage);
        }

        /// <summary>
        /// Save file AS
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> SaveAs(IFormFile file)
        {
            string pathForSaveImage = _configuration.GetValue<string>("Paths:ImagesUploadPath");
            return await SaveAs(file, pathForSaveImage);
        }

        /// <summary>
        /// Save file AS
        /// </summary>
        /// <param name="file"></param>
        /// <param name="pathForSaveImage"></param>
        /// <returns></returns>
        private async Task<string> SaveAs(IFormFile file, string pathForSaveImage)
        {
            if (file != null && file.Length > 0)
            {
                //_webHostingEnvironment
                var FileExtension = Path.GetExtension(file.FileName);

                //var InputFileName = Path.GetFileName(file.FileName);
                var InputFileName = Guid.NewGuid() + FileExtension;
                var filePath = string.Format("{0}{1}", pathForSaveImage, InputFileName);

                var serverFilePath = string.Format("{0}{1}", _webHostingEnvironment.WebRootPath, filePath);

                if (!Directory.Exists(string.Format("{0}{1}", _webHostingEnvironment.WebRootPath, pathForSaveImage)))
                {
                    Directory.CreateDirectory(string.Format("{0}{1}", _webHostingEnvironment.WebRootPath, pathForSaveImage));
                }

                using (var fileStream = new FileStream(serverFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return filePath;
            }
            return null;
        }
    }
}
