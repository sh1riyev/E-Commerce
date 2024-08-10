using System;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.DTOs.UserDto
{
	public class GetUserProductTags
	{
        public string Id { get; set; }
        public string TagName { get; set; }
        public string TagId { get; set; }
        public GetUserProductTags()
		{
		}
	}
}

