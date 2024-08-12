using System;
using AutoMapper;
using E_Commerce.Business.DTOs.SliderDto;
using 	E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class SliderProfile:Profile
	{
		public SliderProfile()
		{
			CreateMap<CreateSliderDto, Slider>().ReverseMap();
			CreateMap<UpdateSliderDto, Slider>().ReverseMap();
			CreateMap< Slider, GetSliderByAdminDto>().ReverseMap();
			CreateMap< Slider, GetSliderDto>().ReverseMap();
        }
	}
}

