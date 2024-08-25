using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using E_Commerce.Business.Interfaces;

namespace E_Commerce.Business.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnviorment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviorment = webHostEnvironment;
        }

        public string CreateImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + ".jpeg";
            string path = Path.Combine(_webHostEnviorment.WebRootPath, "images", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }

        public void DeleteImage(string fileName)
        {
            if (File.Exists(Path.Combine(_webHostEnviorment.WebRootPath, "images", fileName)))
            {
                File.Delete(Path.Combine(_webHostEnviorment.WebRootPath, "images", fileName));
            }
        }

        public bool IsImage(IFormFile file)
        {
            return file.ContentType.Contains("image") ? true : false;
        }

        public bool IsLengthSuit(IFormFile file, int value)
        {
            return file.Length / 1024 < value ? true : false;
        }
    }
}

