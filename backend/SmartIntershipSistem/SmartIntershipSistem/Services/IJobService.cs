using SmartIntershipSistem.DTOs;

namespace SmartIntershipSistem.Services
{
    public interface IJobService
    {
        Task<JobResponseDto> UpdateJob(CreateUpdateDto updateDto,Guid jobId);
        Task<bool> DeleteJob(Guid jobId);
        Task<JobResponseDto> GetJob(Guid jobId);
        Task<List<JobResponseDto>> GetAllJobs();
        Task<JobResponseDto> CreateJob(CreateUpdateDto createDto,Guid companyId);
    }
}
