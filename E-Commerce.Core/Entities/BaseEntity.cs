using System;
namespace E_Commerce.Core.Entities
{
	public class BaseEntity
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public BaseEntity()
		{
		}
	}
}

