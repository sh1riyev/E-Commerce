using System;
namespace E_Commerce.Core.Entities
{
	public class Setting : BaseEntity
	{
        public string Key { get; set; }
        public string Value { get; set; }
        public Setting()
		{
		}
	}
}

