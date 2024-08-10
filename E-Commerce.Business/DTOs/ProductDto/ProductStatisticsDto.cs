using System;
namespace E_Commerce.Business.DTOs.ProductDto
{
	public class ProductStatisticsDto
	{
		public int LastDayCreatedCount { get; set; }
		public int LastDayDeletedCount { get; set; }
		public int LastDayUpdatedCount { get; set; }
        public int LastWeekCreatedCount { get; set; }
        public int LastWeekDeletedCount { get; set; }
        public int LastWeekUpdatedCount { get; set; }
        public int LastMonthCreatedCount { get; set; }
        public int LastMonthDeletedCount { get; set; }
        public int LastMonthUpdatedCount { get; set; }
        public int DeletedCount { get; set; }
        public ProductStatisticsDto()
		{
		}
	}
}

