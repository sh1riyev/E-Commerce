using System;
namespace E_Commerce.Core.Entities
{
	public class Slider : BaseEntity
	{
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Information { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public Slider()
		{
		}
	}
}

