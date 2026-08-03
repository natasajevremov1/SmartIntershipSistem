using Microsoft.EntityFrameworkCore;
using SmartIntershipSistem.Data;
using SmartIntershipSistem.DTOs;
using SmartIntershipSistem.Models;
using System.Text.Json.Serialization;

namespace SmartIntershipSistem.Services
{
    public class JobService:IJobService
    {
        private readonly AppDbContext _appDbContext;
        public JobService(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        
        }

        public async Task<JobResponseDto> CreateJob(CreateUpdateDto createDto, Guid companyId)
        {
            var company = await _appDbContext.Companies.FirstOrDefaultAsync(u => u.UserId == companyId);
            if (company == null)
            {

                throw new Exception("Company not found");
            }
            if (createDto.ApplicationDeadline < DateTime.Now)
            {
                throw new Exception("Advertisement has expired");
            }
            var job = new Job {Id=Guid.NewGuid(), NameJob = createDto.NameJob, CompanyId = companyId,Company=company, JobDescription = createDto.JobDescription, Status = createDto.Status, ExperienceLevel = createDto.ExperienceLevel, ApplicationDeadline = createDto.ApplicationDeadline };
            await _appDbContext.Jobs.AddAsync(job);
            await _appDbContext.SaveChangesAsync();
            return new JobResponseDto { Id=job.Id,CompanyName = company.CompanyName, NameJob = createDto.NameJob, ApplicationDeadline = createDto.ApplicationDeadline, Status = createDto.Status, ExperienceLevel = createDto.ExperienceLevel,JobDescription=createDto.JobDescription};

        }

        public async Task<bool> DeleteJob(Guid jobId)
        {
            bool isDelete=false;
            var job=await _appDbContext.Jobs.FirstOrDefaultAsync(j=>j.Id == jobId);
            if (job == null)
            {
                throw new Exception("Job with this ID do not exists in base");

            }
            else
            {
                _appDbContext.Jobs.Remove(job);
               await  _appDbContext.SaveChangesAsync();
                isDelete=true;
            }
            return isDelete;


        }

        public async Task<List<JobResponseDto>> GetAllJobs()
        {
            var  jobs =await  _appDbContext.Jobs.Include(j => j.Company).ToListAsync();
            return jobs.Select(j=>new JobResponseDto { Id=j.Id,NameJob=j.NameJob,JobDescription=j.JobDescription,ApplicationDeadline=j.ApplicationDeadline,
            ExperienceLevel=j.ExperienceLevel,Status=j.Status,CompanyName=j.Company.CompanyName}).ToList();
        }

        public async Task<JobResponseDto> GetJob(Guid jobId)
        {
            var job=await _appDbContext.Jobs.Include(j=>j.Company).FirstOrDefaultAsync(j=>j.Id==jobId);
            if(job == null)
            {
                throw new Exception("Job with this ID do not exist in base");
            }
            return new JobResponseDto
            {
                NameJob = job.NameJob,
                Id = jobId,
                JobDescription = job.JobDescription,
                ApplicationDeadline = job.ApplicationDeadline,
                ExperienceLevel = job.ExperienceLevel,
                Status = job.Status,
                CompanyName = job.Company.CompanyName
            };
        }

        public async Task<JobResponseDto> UpdateJob(CreateUpdateDto updateDto, Guid jobId)
        {
            var job=await _appDbContext.Jobs.Include(j=>j.Company).FirstOrDefaultAsync(j=>j.Id==jobId);
            if(job == null)
            {
                throw new Exception("This job do not exist in base.");
            }
            if (updateDto.ApplicationDeadline < DateTime.Now)
            {
                throw new Exception("Advertisement has exired");
            }

            job.NameJob = updateDto.NameJob;
            job.JobDescription = updateDto.JobDescription;
            job.ExperienceLevel = updateDto.ExperienceLevel;
            job.Status = updateDto.Status;
            job.ApplicationDeadline = updateDto.ApplicationDeadline;
                
            await _appDbContext.SaveChangesAsync();

            return new JobResponseDto
            {
                Id = jobId,
                NameJob = job.NameJob,
                JobDescription = job.JobDescription,
                ApplicationDeadline = job.ApplicationDeadline,
                ExperienceLevel = job.ExperienceLevel,
                Status = job.Status,
                CompanyName = job.Company.CompanyName
            };
        }
    }
}
