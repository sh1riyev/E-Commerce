using System;
namespace E_Commerce.Core.Entities
{
	public class Subscribe : BaseEntity
	{
        public string Email { get; set; }
        public string Gender { get; set; }
        public Subscribe()
		{
		}
	}
}

