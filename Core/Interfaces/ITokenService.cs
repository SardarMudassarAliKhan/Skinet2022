using Skinet.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
