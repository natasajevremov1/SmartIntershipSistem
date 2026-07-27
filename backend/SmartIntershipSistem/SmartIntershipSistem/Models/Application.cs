namespace SmartIntershipSistem.Models
{
    public enum ApplicationStatus{Pending,Shortlisted,Accepted,Rejected}
    public class Application
    {
        public Guid Id { get; set; }
        public Candidate Candidate { get; set; } // who signed up
        public Job Job { get; set; } //what did he sign up for
        public DateTime DateOfApplication { get; set; } 
        public ApplicationStatus Status { get; set; }
        public Guid CandidateId { get; set; }
        public Guid JobId { get; set; }

    }
}
