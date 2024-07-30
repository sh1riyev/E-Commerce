using System;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Business.Interfaces
{
	public interface IPhotoAccessor
	{
        Task<ImageUploadResult> AddPhoto(IFormFile file);
        Task<DeletionResult> DeletePhoto(string publicId);
    }
}

