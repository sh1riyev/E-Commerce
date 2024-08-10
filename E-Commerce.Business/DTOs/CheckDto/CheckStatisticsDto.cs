using System;
namespace E_Commerce.Business.DTOs.CheckDto
{
	public class CheckStatisticsDto
	{
		public int ProsessingCount { get; set; }
		public int PreparingCount { get; set; }
		public int ShipedCount { get; set; }
		public int DeliveredCount { get; set; }
		public int DeletedCount { get; set; }
    }
}

