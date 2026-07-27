namespace SmartIntershipSistem.Models
{
    public class CandidateSkill
    {
        public Candidate Candidate { get; set; }
        public Guid CandidateId { get; set; }
        public Skill Skill { get; set; }
        public Guid SkillId { get; set; }
    }
}
