namespace SmartIntershipSistem.Models
{
    public class JobSkill
    {
        public Job Job { get; set; }
        public Guid JobId { get; set; }
        public Skill Skill { get; set; }
        public Guid SkillId { get; set; }
    }
}
