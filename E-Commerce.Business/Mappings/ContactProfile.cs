using System;
using AutoMapper;
using E_Commerce.Business.DTOs.ContactDto;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Mappings
{
	public class ContactProfile:Profile
	{
		public ContactProfile()
		{
			CreateMap<CreateContactDto, Contact>().ReverseMap();
			CreateMap<UpdateContactDto, Contact>().ReverseMap();
			CreateMap<GetContactByAdminDto, Contact>().ReverseMap();
			CreateMap<GetContactDto, Contact>().ReverseMap();
        }
	}
}

