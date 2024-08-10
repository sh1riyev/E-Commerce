using System;
namespace E_Commerce.Business.DTOs.TagDto
{
	public class UpdateTagDto
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateTagDto()
		{
		}
	}
}

