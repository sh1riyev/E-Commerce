using System;
using E_Commerce.Core.Entities;

namespace E_Commerce.Business.Interfaces
{
	public interface ITokenService
	{
        string CreateToken(AppUser user, IList<string> roles);
    }
}

