using SmartIntershipSistem.Models;
namespace SmartIntershipSistem.DTOs
{
    public class CreateUpdateDto
    {
        public string NameJob { get; set; }
        public string JobDescription { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public Status Status { get; set; }
        public DateTime ApplicationDeadline { get; set; }

    }
}
