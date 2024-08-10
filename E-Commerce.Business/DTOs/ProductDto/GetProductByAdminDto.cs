using System;


namespace E_Commerce.Business.DTOs.ProductDto
{
	public class GetProductByAdminDto
	{
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public string Color { get; set; }
        public double Weight { get; set; }
        public string Material { get; set; }
        public string Name { get; set; }
        public int ReviewCount { get; set; }
        public double Price { get; set; }
        public double SalePercentage { get; set; }
        public string Size { get; set; }
        public int StarsCount { get; set; }
        public double Tax { get; set; }
        public int Count { get; set; }
        public string Content { get; set; }
        public string ProductCode { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public string SellerId { get; set; }
        public string SellerFullName { get; set; }
        public List<GetProductImagesDto> ProductImages { get; set; }
        public List<GetProductTagsDto> ProductTags { get; set; }
        public int WishlistCount { get; set; }
        public int CommentCount { get; set; }
        public int BasketCount { get; set; }

        //inovations

        public bool IsAccepted { get; set; }
        public Nullable<DateTime> AcceptedDate { get; set; }
        public bool IsVIP { get; set; }
        public int VipDegre { get; set; }
        public Nullable<DateTime> VipDate { get; set; }
        public bool IsDonation { get; set; }
        public GetProductByAdminDto()
		{
		}
	}
}

