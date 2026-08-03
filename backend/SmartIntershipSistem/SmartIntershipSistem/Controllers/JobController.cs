using Microsoft.AspNetCore.Mvc;
using SmartIntershipSistem.Data;
using SmartIntershipSistem.DTOs;
using SmartIntershipSistem.Services;
using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SmartIntershipSistem.Controllers
{
    [ApiController]                 // Enables API behaviors like automatic model validation
    [Route("job/[controller]")]     // Sets the route to /api/products

    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;

        }


        [HttpGet("getall-job")]
        public async Task<ActionResult<JobResponseDto>> GetAllJobs()
        {
            return Ok(await _jobService.GetAllJobs());
        }

        [HttpPost("create-job")]
        public async Task<ActionResult<JobResponseDto>> CreateJob(CreateUpdateDto createUpdateDto)
        {
            //var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            //izvlacimo id iz tokena

            var companyId = Guid.Parse(User.FindFirstValue("nameid")!); if (createUpdateDto == null)
            {
                return BadRequest();
            }
            return Ok(await _jobService.CreateJob(createUpdateDto, companyId));

        }

        [HttpPut("update/{jobId}")]
        public async Task<ActionResult<JobResponseDto>>UpdateJob(CreateUpdateDto createUpdateDto,Guid jobId)
        {
            if(createUpdateDto == null)
            {
                return BadRequest();
            }
            return Ok(await _jobService.UpdateJob(createUpdateDto, jobId));
        }
        [HttpDelete("deletejob/{jobId}")]
        public async Task<ActionResult<bool>>Delete(Guid jobId)
        {
            return Ok(await _jobService.DeleteJob(jobId));

        }
        [HttpGet("getjob/{jobId}")]
        public async Task<ActionResult<JobResponseDto>>GetJob(Guid jobId)
        {
            return Ok(await _jobService.GetJob(jobId));
        }
    }
}
