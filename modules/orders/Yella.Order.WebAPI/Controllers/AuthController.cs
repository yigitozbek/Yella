using Microsoft.AspNetCore.Mvc;
using Yella.Identity.Dtos;
using Yella.Identity.Helpers.Security.JWT;
using Yella.Identity.Interfaces;
using Yella.Order.Domain.Identities;
using Yella.Utilities.Results;

namespace Yella.Order.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService<User, Role> _authService;

        public AuthController(IAuthService<User, Role> authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IDataResult<AccessToken>> Login(LoginDto input)
        {
            var result = await _authService.LoginAsync(input);
            return result;

        }



    }
}
