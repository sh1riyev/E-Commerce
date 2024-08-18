using System;
namespace E_Commerce.Core.Entities
{
	public class Compaigns : BaseEntity
	{
        public string Headling { get; set; }
        public string Content { get; set; }
        public string Info { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }
        public double Sale { get; set; }
        public Compaigns()
		{
		}
	}
}

