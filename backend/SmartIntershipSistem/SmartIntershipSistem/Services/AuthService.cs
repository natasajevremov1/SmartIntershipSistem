using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SmartIntershipSistem.Data;
using SmartIntershipSistem.DTOs;
using SmartIntershipSistem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user=await _dbContext.Users.FirstOrDefaultAsync(u=>u.Email==loginDto.Email);

            if(user==null)
            {
                return new AuthResponseDto { Message = "User with this email don't exists in base", JWTToken = null };

            }
            else
            {
                var PasswordHash = new PasswordHasher<User>();
                var hashedPassword = PasswordHash.VerifyHashedPassword( user,user.Password,loginDto.Password);
                if (hashedPassword == PasswordVerificationResult.Failed)
                {
                    return new AuthResponseDto { Message = "Passwords doesn't match", JWTToken = null };
                }else 
                {
                    var token = GenerateJWTToken(user);
                   return new AuthResponseDto { Message="Succesfully login",JWTToken = token,Role=user.Role};
                }
            }

        }

        public async Task<AuthResponseDto> RegisterCandidate(RegisterCandidateDto registerCandidateDto)
        {
            //proveri lozinke
            if(registerCandidateDto.Password != registerCandidateDto.ConfirmPassword)
            {
                return new AuthResponseDto { Message = "Passwords do not match.", JWTToken=null };
            }
            //proveri postoji li vec email taj u bazu
            if (await _dbContext.Users.AnyAsync(u => u.Email == registerCandidateDto.Email)){
                return new AuthResponseDto { Message = "This email already exists.", JWTToken = null };
            }
            //hesuj loiznku
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword=passwordHasher.HashPassword(null, registerCandidateDto.Password);
            //napravi usera
            var newUser = new User { Id=Guid.NewGuid(),Name=registerCandidateDto.Name, Email=registerCandidateDto.Email, Username=registerCandidateDto.Username,Lastname=registerCandidateDto.Lastname, Password=hashedPassword,Role=Role.Candidate };
            await _dbContext.Users.AddAsync(newUser);
            
            //kreiraj kandidata
            var newCandidate=new Candidate { UserId=newUser.Id,User=newUser,Education=" ",CV=" ",GitHub=" "};
            await _dbContext.Candidates.AddAsync(newCandidate);
            await _dbContext.SaveChangesAsync();

           var token=  GenerateJWTToken(newUser);
            return new AuthResponseDto { Message="Succesfully registration!",JWTToken=token,Role=newUser.Role };
        }

        public async Task<AuthResponseDto> RegisterCompany(RegisterCompanyDto registerCompanyDto)
        {
           //proveravamo dal se lozinke poklapaju
           if(registerCompanyDto.Password != registerCompanyDto.ConfirmPassword)
            {
                return new AuthResponseDto { Message = "Passwords do not complements", JWTToken=null};
            }
            //dal je email vec u bazi
            if (await _dbContext.Users.AnyAsync(u => u.Email == registerCompanyDto.Email))
            {
                return new AuthResponseDto { Message = "This Email already exists in base", JWTToken = null };
            }

            //hesovanje lozinke
            var passwordHasher = new PasswordHasher<User>();
            var hahshedPassword = passwordHasher.HashPassword(null, registerCompanyDto.Password);

            //napravi usera
            var newUser=new User {Id=Guid.NewGuid(), Password=hahshedPassword,Name=registerCompanyDto.Name,Email=registerCompanyDto.Email,Username=registerCompanyDto.Username,Lastname=registerCompanyDto.Lastname,Role=Role.Company};
            await _dbContext.Users.AddAsync(newUser);

            //napravi kompaniju
            var newCompany = new Company { UserId = newUser.Id,User=newUser, CompanyName = registerCompanyDto.CompanyName, Description = "", WebSite = "" };
            await _dbContext.Companies.AddAsync(newCompany);
            await _dbContext.SaveChangesAsync();
            var token = GenerateJWTToken(newUser);

            return new AuthResponseDto { Message ="Succesfully registration company",JWTToken= token,Role=newUser.Role};

        }


        private string GenerateJWTToken(User user)
        {
            //procitaj jwt podesavanja iz konf
            var settings = _configuration.GetSection("JwtSettings");
            //kreiraj SymmetricSecurityKey koristeci secret key iz setttingsa 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings["SecretKey"]!)); //ovo ! na kraju znaci da ne moze sig biiti null

            //kreiramo claims

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role,user.Role.ToString()),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub,user.Id.ToString())
                }),

                Expires=DateTime.UtcNow.AddDays(double.Parse(settings["ExpiryInDays"]!)),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Issuer = settings["Issuer"],
                Audience = settings["Audience"]

            };

            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }
    }
}
