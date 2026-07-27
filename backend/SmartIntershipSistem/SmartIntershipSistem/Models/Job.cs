namespace SmartIntershipSistem.Models
{
    public enum ExperienceLevel { Junior, Medior, Senior }
    public enum Status { activate,closed}

    public class Job
    {
        public Guid Id { get; set; }
        public string NameJob { get; set; }
        public string JobDescription { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public Status Status { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime ApplicationDeadline { get; set; }

        
    }
}
