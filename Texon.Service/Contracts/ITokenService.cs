using System.Collections.Generic;
using Texon.Domin.Entities.Auth;

namespace Texon.Service.Contracts
{
    public interface ITokenService
    {
         string GetToken(ApplicationUser user, IList<string> token);



    }
}
