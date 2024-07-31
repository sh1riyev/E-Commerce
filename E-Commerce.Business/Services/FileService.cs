using System;
using E_Commerce.Business.Interfaces;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.Services
{
	public class FileService : IFileService
	{
		public FileService()
		{
		}

        public string CreateImage(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public void DeleteImage(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool IsImage(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public bool IsLengthSuit(IFormFile file, int value)
        {
            throw new NotImplementedException();
        }
    }
}

