using System.Security.Claims;
using LYC.Helpers;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace LYC.Controllers
{
    public class BaseController : ControllerBase
    {
        internal UserJwtVM GetAuthorizationHeader()
        {
            string authHeader = Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(authHeader))
            {
                authHeader = authHeader.Replace("Bearer ", "");

                var tokenInfo = JWTHelper.GetTokenInfo(authHeader);

                UserJwtVM userJwtVM = new UserJwtVM
                {
                    UserId = tokenInfo[ClaimTypes.NameIdentifier],
                    UserEmail = tokenInfo[ClaimTypes.Email],
                    UserName = tokenInfo[ClaimTypes.Name],
                    UserRole = tokenInfo[ClaimTypes.Role],
                };

                return userJwtVM;
            }

            return new UserJwtVM();
        }

        internal string GetTokenFromHeader()
        {
            string authHeader = Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(authHeader))
            {
                authHeader = authHeader.Replace("Bearer ", "");

                return authHeader;
            }

            return string.Empty;
        }
    }
}