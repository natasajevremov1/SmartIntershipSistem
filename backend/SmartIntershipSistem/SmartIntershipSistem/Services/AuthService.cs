using SmartIntershipSistem.Data;
using SmartIntershipSistem.DTOs;

namespace SmartIntershipSistem.Services
{
   
    public class AuthService : IAuthService
    { //bez ovoga bi svaka ova metoda morala da kreira posebno konekciju sa bazom i cita konfig
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthService(AppDbContext dbContext,IConfiguration configuration) 
        {
            _dbContext = dbContext;
            _configuration = configuration;
        
        }
        public Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> RegisterCandidate(RegisterCandidateDto registerCandidateDto)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponseDto> RegisterCompany(RegisterCompanyDto registerCompanyDto)
        {
            throw new NotImplementedException();
        }
    }
}
