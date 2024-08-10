using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Business.DTOs.CheckDto
{
	public class UpdateCheckDto
	{
        public string Id { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
        public UpdateCheckDto()
		{
		}
	}
}

