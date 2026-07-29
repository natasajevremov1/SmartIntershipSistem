using SmartIntershipSistem.DTOs;

namespace SmartIntershipSistem.Services
{
    public interface IAuthService
    {
        
        Task<AuthResponseDto> Login(LoginDto loginDto);

        Task<AuthResponseDto>RegisterCandidate(RegisterCandidateDto  registerCandidateDto);
        Task<AuthResponseDto>RegisterCompany(RegisterCompanyDto  registerCompanyDto);
    }
}
