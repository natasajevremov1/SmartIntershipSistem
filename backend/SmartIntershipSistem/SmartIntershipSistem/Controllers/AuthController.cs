using Microsoft.AspNetCore.Mvc;
using SmartIntershipSistem.DTOs;
using SmartIntershipSistem.Services;

namespace SmartIntershipSistem.Controllers
{

    [ApiController]                 // Enables API behaviors like automatic model validation
    [Route("auth/[controller]")]     // Sets the route to /api/products
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;

        }

        [HttpPost("register-candidate")]
        public async Task<ActionResult<AuthResponseDto>> RegisterCandidate(RegisterCandidateDto registerCandidateDto)
        {
            if (registerCandidateDto == null)
            {
                return BadRequest();
            }
            return Ok(await _authService.RegisterCandidate(registerCandidateDto));
        }

        [HttpPost("register-company")]
        public async Task<ActionResult<AuthResponseDto>> RegisterCOmpany(RegisterCompanyDto registerCompanyDto)
        {
            if (registerCompanyDto == null) { return BadRequest(); }
            return Ok(await _authService.RegisterCompany(registerCompanyDto));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto login)
        {
            if (login == null) { return BadRequest(); }
            return Ok(await _authService.Login(login));

        }
    }
}
