using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories;
using Api.Core;
using Api.Service;


namespace Api.Controller
{
    [ApiController]
    [Route("/api/auth/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly JwtService _JwtService;
        private readonly IRegisterService RegisterService;
        private readonly ILoginService LoginService;

        public AuthController(
            UserRepository _userRepository,
            JwtService _JwtService,
            IRegisterService RegisterService,
            ILoginService LoginService)
        {
            this._userRepository = _userRepository;
            this._JwtService = _JwtService;
            this.RegisterService = RegisterService;
            this.LoginService = LoginService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterServiceRequest request)
        {
            var response = await RegisterService.ExecuteAsync(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginServiceRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            var response = await LoginService.ExecuteAsync(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            var token = _JwtService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}